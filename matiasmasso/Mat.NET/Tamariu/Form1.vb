Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Reflection.Emit
Imports System.Threading

Public Class Form1
    Dim url As String = "2.139.175.182/" ' "192.168.1.77"
    Dim port As String = "1081"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub


    'Cross-thread safe code using InvokeRequired Pattern
    Private Sub SetIndicators(ByVal ok As Boolean)

        If TextBox1.InvokeRequired Then
            Dim d As New Action(Of Boolean)(AddressOf SetIndicators)
            Me.Invoke(d, ok)
        Else
            If ok = True Then
                TextBox1.Text = "Online!"
                TextBox1.ForeColor = Color.LimeGreen
            Else
                TextBox1.Text = "Offline!"
                TextBox1.ForeColor = Color.DarkRed
            End If
        End If
    End Sub



    Private Function checkport(hostname As String, port As Integer
                             ) As Boolean
        Dim client As New TcpClient(AddressFamily.InterNetwork)


        client.BeginConnect(hostname, port,
                        Sub(x)
                            Dim tcp As TcpClient = CType(x.AsyncState, TcpClient)
                            Try
                                tcp.EndConnect(x)
                                SetIndicators(True)

                            Catch ex As Exception
                                'error
                                SetIndicators(False)

                            End Try
                            tcp.Close()
                        End Sub, client
                        )


        Return (False)
    End Function
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'result can be returned late according connection timeout
        checkport(url, port)
    End Sub


End Class
