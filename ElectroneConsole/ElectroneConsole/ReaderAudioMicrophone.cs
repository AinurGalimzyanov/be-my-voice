using NAudio.Lame;
using NAudio.Wave;

namespace ConsoleApp1;

public class ReaderAudioMicrophone
{
    public void Read(int fileNumber)
    {
        var waveIn = new WaveInEvent();

        waveIn.WaveFormat = new WaveFormat(44100, 16, 2);

        var outputFileName = $"{fileNumber}microphone.mp3";
        LameMP3FileWriter writer = null;

        waveIn.DataAvailable += (sender, e) =>
        {
            if (writer == null)
            {
                writer = new LameMP3FileWriter(outputFileName, waveIn.WaveFormat, LAMEPreset.STANDARD);
            }

            writer.Write(e.Buffer, 0, e.BytesRecorded);
        };

        waveIn.StartRecording();

        Console.WriteLine("Запись звука с микрофона...");

        // Ждем 5 секунд
        Thread.Sleep(5000);

        waveIn.StopRecording();

        writer?.Dispose();

        Console.WriteLine($"Запись завершена. Аудио сохранено в файл: {outputFileName}");
    }
}
