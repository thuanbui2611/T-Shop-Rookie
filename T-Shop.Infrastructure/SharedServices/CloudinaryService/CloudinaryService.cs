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

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account
        (
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new CloudinaryDotNet.Cloudinary(acc);
    }

    public async Task<CloudinaryResult> AddPhotoAsync(IFormFile? file)
    {
        if (file is not { Length: > 0 }) return null;

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new(file.FileName, stream),
            Transformation = new Transformation().Height(800).Width(800).Crop("fill").Gravity("face")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        var result = new CloudinaryResult()
        {
            PublicID = uploadResult.PublicId,
            ImageUrl = uploadResult.SecureUrl.AbsoluteUri
        };
        return result;
    }

    public async Task<string> DeletePhotoAsync(string publicID)
    {
        var deleteParams = new DeletionParams(publicID);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result.Result;
    }
}
