namespace LoadCamera;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();

	}

    async void grabImgBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        var bytes = await TakePhoto();
        imageViewer.Source = ImageSource.FromStream(() => new MemoryStream(bytes));



    }

    public static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    public async Task<byte[]> TakePhoto()
    {
        
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                //string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using var sourceStream = await photo.OpenReadAsync();
                //using FileStream localFileStream = File.OpenWrite(localFilePath);

                //await sourceStream.CopyToAsync(localFileStream);

                return ReadFully(sourceStream);
            }
        }
        return null;
    }

}


