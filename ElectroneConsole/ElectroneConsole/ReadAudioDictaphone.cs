using NAudio.Lame;
using NAudio.Wave;

namespace ConsoleApp1;

public class ReadAudioDictaphone
{
    public void Read(ref int fileNumber)
    {
        var loopbackCapture = new WasapiLoopbackCapture();

        var outputFileName = $"{fileNumber}speakers.mp3";
        LameMP3FileWriter writer = null;

        loopbackCapture.DataAvailable += (sender, e) =>
        {
            if (writer == null)
            {
                writer = new LameMP3FileWriter(outputFileName, loopbackCapture.WaveFormat, LAMEPreset.STANDARD);
            }

            writer.Write(e.Buffer, 0, e.BytesRecorded);
        };

        loopbackCapture.StartRecording();

        Console.WriteLine("Запись звука с динамика компьютера...");

        Thread.Sleep(5000);

        loopbackCapture.StopRecording();

        writer?.Dispose();

        Console.WriteLine($"Запись завершена. Аудио сохранено в файл: {outputFileName}");
    }
}
