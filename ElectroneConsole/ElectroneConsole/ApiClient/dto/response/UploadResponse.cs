namespace ElectroneConsole.ApiClient.dto.response;

public class UploadResponse
{
    public int Status;
    public UploadResult? UploadResult;
}

public class UploadResult
{
    public string? RequestFileId;
}