using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using T_Shop.Application.Common.ServiceInterface;
using T_Shop.Domain.Entity.ServiceEntity.Cloudinary;

namespace T_Shop.Infrastructure.SharedServices.Cloudinary;
public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    private readonly string folder;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account
        (
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        folder = config.Value.Folder;
        _cloudinary = new CloudinaryDotNet.Cloudinary(acc);
    }

    public async Task<CloudinaryResult> AddImageAsync(IFormFile file)
    {
        if (file is not { Length: > 0 }) return null;

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new(file.FileName, stream),
            Transformation = new Transformation().Height(800).Width(800).Crop("fill").Gravity("face"),
            Folder = folder
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        var result = new CloudinaryResult()
        {
            PublicID = uploadResult.PublicId,
            ImageUrl = uploadResult.SecureUrl.AbsoluteUri
        };
        return result;
    }

    public async Task<List<CloudinaryResult>> AddImagesAsync(IFormFileCollection files)
    {
        if (files.Count == 0) return null;
        List<CloudinaryResult> result = new();
        foreach (var file in files)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new(file.FileName, stream),
                Transformation = new Transformation().Height(800).Width(800).Crop("fill").Gravity("face"),
                Folder = folder
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            var imageAdded = new CloudinaryResult()
            {
                ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                PublicID = uploadResult.PublicId,
            };
            result.Add(imageAdded);
        }

        return result;
    }

    public async Task<bool> UpdateImageAsync(IFormFile file, string publicID)
    {
        if (file is not { Length: > 0 } || publicID is null) return false;

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new(file.FileName, stream),
            Transformation = new Transformation().Height(800).Width(800).Crop("fill").Gravity("face"),
            PublicId = publicID,
            Overwrite = true
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult.Error is not null) throw new Exception(uploadResult.Error.Message);

        return true;
    }

    public async Task<string> DeleteImageAsync(string publicID)
    {
        var deleteParams = new DeletionParams(publicID);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result.Result;
    }
}
