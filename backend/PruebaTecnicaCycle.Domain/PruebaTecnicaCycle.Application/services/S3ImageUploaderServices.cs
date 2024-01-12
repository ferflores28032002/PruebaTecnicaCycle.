using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

public class S3ImageUploaderServices
{
    private readonly AmazonS3Client _s3Client;
    private const string BucketName = "firstbucketnode";

    public S3ImageUploaderServices()
    {

        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.USEast2
        };

        //esta informacion importante se debe colocar en variables de entorno o archivos de configuraciones (key, etc)
        _s3Client = new AmazonS3Client("AKIA3ARXAXIQ4WHYZIAF", "qYgvAZ1hOXR7VrTIPLPNtj2wW85u7zFyMvOKn9ke", config);
    }

    public async Task<UploadResult> UploadImageAsync(string filePath)
    {
        string keyName = Guid.NewGuid().ToString(); // Generate a unique key name using a GUID.

        try
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = BucketName,
                Key = keyName,
                FilePath = filePath,
                ContentType = "image/jpeg"
            };

            PutObjectResponse response = await _s3Client.PutObjectAsync(putRequest);

            return new UploadResult
            {
                Success = true,
                Message = "Image successfully uploaded to S3 bucket.",
                ImageUrl = $"https://{BucketName}.s3.amazonaws.com/{keyName}"
            };
        }
        catch (AmazonS3Exception e)
        {
            return new UploadResult
            {
                Success = false,
                Message = $"Error encountered on server. Message: '{e.Message}' when writing an object"
            };
        }
        catch (Exception e)
        {
            return new UploadResult
            {
                Success = false,
                Message = $"Unknown error encountered on server. Message: '{e.Message}' when writing an object"
            };
        }
    }
}

public class UploadResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string ImageUrl { get; set; }
}
