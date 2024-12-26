using System.IO.Ports;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    private SerialPort serialPort;
    public static string receivedData = ""; // Make this static so TileController can access it
    private bool isPortOpen = false;
    public string portName = "COM3";  // Replace with your COM port
    public int baudRate = 9600;

    void Start()
    {
        OpenSerialPort();
    }

    // Method to open the serial port
    void OpenSerialPort()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();  // Open the port
            serialPort.ReadTimeout = 10;  // Set timeout for reading data (in milliseconds)
            isPortOpen = true;
            Debug.Log("Serial Port Opened");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (isPortOpen)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    receivedData = serialPort.ReadLine().Trim(); // Read and trim data
                    Debug.Log("Received: " + receivedData); // Debug log to check received data
                }
            }
            catch (System.TimeoutException)
            {
                // Timeout occurred - no data received
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error reading from serial port: " + ex.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close(); // Close the port when the application quits
            isPortOpen = false;
        }
    }
}
