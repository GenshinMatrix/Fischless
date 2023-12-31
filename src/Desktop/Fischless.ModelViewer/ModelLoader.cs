﻿using Fischless.Logging;
using Fischless.ModelViewer.MMD;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Fischless.ModelViewer;

public class ModelLoader : IDisposable
{
    private readonly PMXProvider loader = new();
    private PMXFormat format;

    public async Task<Model3DGroup> LoadModel(string path, Func<string[], Task<string>> selector = null)
    {
        if (!File.Exists(path))
        {
            return null!;
        }

        try
        {
            loader.Load(path);

            format = (await loader.GetPMXFormat(path, selector)) ?? throw new FormatException("Unsupported format");

            MeshCreationInfo creation_info = CreateMeshCreationInfoSingle();

            int mats = format.material_list.material.Length;

            Model3DGroup models = new();

            for (int i = 0, i_max = creation_info.value.Length; i < i_max; ++i)
            {
                try
                {
                    // format_.face_vertex_list.face_vert_indexを[start](含む)から[start+count](含まず)迄取り出し
                    // 頂点リアサインインデックス変換
                    int[] indices = creation_info.value[i].plane_indices.Select(x => (int)creation_info.reassign_dictionary[x]).ToArray();
                    MeshGeometry3D mesh = new()
                    {
                        Positions = new Point3DCollection(format.vertex_list.vertex.Select(x => x.pos)),
                        TextureCoordinates = new PointCollection(format.vertex_list.vertex.Select(x => x.uv)),
                    };

                    indices.ToList().ForEach(x => mesh.TriangleIndices.Add(x));

                    uint textureIndex = format.material_list.material[i].usually_texture_index;

                    Material material;

                    if (textureIndex == uint.MaxValue)
                    {
                        material = new DiffuseMaterial(new SolidColorBrush(Color.FromRgb(160, 160, 160)));
                    }
                    else
                    {
                        // Texture
                        ImageSource bitmapImage = loader.GetTexture(format.meta_header.folder, format.texture_list.texture_file[textureIndex]);

#if DEBUG && TEXTURE
                        bitmapImage.Save(format.meta_header.folder + @"\.debug\" + format.texture_list.texture_file[textureIndex] + ".png");
#endif
                        ImageBrush colors_brush = new()
                        {
                            ImageSource = bitmapImage,
                        };
                        material = new DiffuseMaterial(colors_brush);
                    }

                    GeometryModel3D model = new(mesh, material)
                    {
                        BackMaterial = material,
                    };
                    models.Children.Add(model);
                }
                catch (Exception e)
                {
                    Log.Warning(e.ToString());
                }
            }
            return models;
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
        return null!;
    }

    public MeshCreationInfo CreateMeshCreationInfoSingle()
    {
        MeshCreationInfo result = new()
        {
            // 全マテリアルを設定
            value = CreateMeshCreationInfoPacks(),
            // 全頂点を設定
            all_vertices = Enumerable.Range(0, format.vertex_list.vertex.Length).Select(x => (uint)x).ToArray()
        };
        // 頂点リアサインインデックス用辞書作成
        result.reassign_dictionary = new Dictionary<uint, uint>(result.all_vertices.Length);
        for (uint i = 0, i_max = (uint)result.all_vertices.Length; i < i_max; ++i)
        {
            result.reassign_dictionary[i] = i;
        }
        return result;
    }

    public MeshCreationInfo.Pack[] CreateMeshCreationInfoPacks()
    {
        uint plane_start = 0;
        // マテリアル単位のMeshCreationInfo.Packを作成する
        return Enumerable.Range(0, format.material_list.material.Length).Select(x =>
        {
            MeshCreationInfo.Pack pack = new()
            {
                material_index = (uint)x,
            };
            uint plane_count = format.material_list.material[x].face_vert_count;
            pack.plane_indices = format.face_vertex_list.face_vert_index.Skip((int)plane_start).Take((int)plane_count).ToArray();
            pack.vertices = pack.plane_indices.Distinct().ToArray();
            plane_start += plane_count;
            return pack;
        }).ToArray();
    }

    public void Dispose()
    {
        loader?.Dispose();
    }
}

public class MeshCreationInfo
{
    public class Pack
    {
        public uint material_index; // マテリアル
        public uint[] plane_indices; // 面
        public uint[] vertices; // 頂点
    }

    public Pack[] value;
    public uint[] all_vertices; // 総頂点
    public Dictionary<uint, uint> reassign_dictionary; // 頂点リアサインインデックス用辞書
}
