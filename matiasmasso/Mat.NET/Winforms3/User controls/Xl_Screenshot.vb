Public Class Xl_Screenshot
    Inherits PictureBox
    Private _label As Label

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Property image As Image
        Get
            Return MyBase.Image
        End Get
        Set(value As Image)
            MyBase.Image = value
            MyBase.SizeMode = PictureBoxSizeMode.Zoom
            setLabel()
        End Set
    End Property

    Private Sub Xl_Screenshot_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        'Dim oImage As Image = Clipboard.GetDataObject()

        If MyBase.Image Is Nothing Then

            If My.Computer.Clipboard.ContainsImage() Then
                Dim grabpicture As System.Drawing.Image = My.Computer.Clipboard.GetImage()
                MyBase.Image = grabpicture
                RaiseEvent AfterUpdate(Me, New MatEventArgs(MyBase.Image))
            Else

                Dim oDlg As New OpenFileDialog
                With oDlg
                    .Title = "Importar captura de pantalla"
                    .Filter = "imatges|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.ico|tots els fitxers (*.*) | *.*"
                    If .ShowDialog Then
                        If .FileName > "" Then
                            MyBase.Load(.FileName)
                            RaiseEvent AfterUpdate(Me, New MatEventArgs(MyBase.Image))
                        End If
                    End If
                End With
            End If
        Else
            If My.Computer.Clipboard.ContainsImage() Then
                Dim grabpicture As System.Drawing.Image = My.Computer.Clipboard.GetImage()
                If grabpicture.GetHashCode = (MyBase.Image.GetHashCode) Then
                    Dim oFrm As New Frm_Picture(MyBase.Image)
                    oFrm.Show()
                Else
                    MyBase.Image = grabpicture
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(MyBase.Image))
                End If
            Else
                Dim oFrm As New Frm_Picture(MyBase.Image)
                oFrm.Show()
            End If
        End If

    End Sub

    Private Sub Xl_Screenshot_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                If MyBase.Image IsNot Nothing Then
                    MyBase.Image = Nothing
                    setLabel()
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                End If
        End Select
    End Sub


    Private Sub setLabel()
        If _label Is Nothing Then
            _label = New Label
            _label.Location = New Point(10, 10)
        End If
        If MyBase.Image Is Nothing Then

            _label.Visible = True
            If My.Computer.Clipboard.ContainsImage() Then
                _label.Text = "Doble clic per adjuntar la captura de pantalla"
            Else
                _label.Text = "Doble clic per importar una captura de pantalla"
            End If
        Else
            _label.Visible = False
        End If
    End Sub
End Class
