using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SignalListener : MonoBehaviour
{
    public SignalSender signal;
    public UnityEvent signalEvent; // Sự kiện Unity sẽ được gọi khi Signal được kích hoạt
    public void OnSignalRaised()
    {
        signalEvent.Invoke(); // Gọi sự kiện Unity để thực hiện các hành động tương ứng
    }
    private void OnEnable()
    {
        // Đăng ký đối tượng Listener với SignalSender để lắng nghe Signal
        signal.RegisterListener(this); 
    }
    private void OnDisable()
    {
        // Hủy đăng ký đối tượng Listener với SignalSender khi không còn cần lắng nghe nữa
        signal.DeRegisterListener(this);
    }
}
