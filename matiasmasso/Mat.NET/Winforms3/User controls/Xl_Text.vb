Imports System.Runtime.InteropServices

Public Class Xl_Text
    Inherits Xl_LookupTextboxButton

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)


    Public Sub Clear()
        MyBase.Value = ""
    End Sub

    Private Sub Xl_LookupCnap_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        OpenNotepad(MyBase.Value)
    End Sub


    Const WM_SETTEXT As Integer = &HC
    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As IntPtr, <MarshalAs(UnmanagedType.LPStr)> lParam As String) As IntPtr
    End Function

    Private Sub OpenNotepad(textToAdd As String)
        'ProcessStartInfo is used to instruct the Process class
        ' on how to start a new process. The UseShellExecute tells
        ' the process class that it (amongst other) should search for the application
        ' using the PATH environment variable.
        Dim pis As ProcessStartInfo = New ProcessStartInfo("notepad.exe")
        pis.UseShellExecute = True

        ' The process class is used to start the process
        ' it returns an object which can be used to control the started process
        Dim notepad As Process = Process.Start(pis)

        ' SendMessage is used to send the clipboard message to notepad's
        ' main window.
        SendMessage(notepad.MainWindowHandle, WM_SETTEXT, IntPtr.Zero, textToAdd)
    End Sub



End Class

