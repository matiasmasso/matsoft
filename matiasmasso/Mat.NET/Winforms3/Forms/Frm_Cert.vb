Public Class Frm_Cert
    Private _Contact As DTOContact
    Private _Cert As DTOCert

    Public Sub New(oContact As DTOContact)
        MyBase.New
        InitializeComponent()
        _Contact = oContact
    End Sub

    Private Async Sub Frm_Cert_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Cert = Await FEB.Cert.Find(_Contact, exs)
        If exs.Count = 0 Then
            If _Cert IsNot Nothing Then
                With _Cert
                    TextBoxPwd.Text = .Pwd
                    DateTimePickerCaduca.Value = .Caduca
                    .Stream = Await FEB.Cert.Stream(_Contact, exs)
                End With
                Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(Await FEB.Cert.Image(_Contact, exs))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .InitialDirectory = FileSystemHelper.PathToMyDocuments
            .Filter = "Certificats|*.pfx,*.p12|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                TextBoxFilename.Text = .FileName
                ButtonOk.Enabled = True
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If TextBoxFilename.Text > "" Then
            If _Cert Is Nothing Then _Cert = New DTOCert(_Contact.Guid)
            If FEB.Cert.ImportFromFile(_Cert, TextBoxFilename.Text, TextBoxPwd.Text, DateTimePickerCaduca.Value, exs) Then
                _Cert.Image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
                If Await FEB.Cert.Update(_Cert, exs) Then
                    Me.Close()
                Else
                    UIHelper.ToggleProggressBar(PanelButtons, False)
                    UIHelper.WarnError(exs, "error al importar certificat" & Environment.NewLine)
                End If
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al importar certificat" & Environment.NewLine)
            End If
        Else
            With _Cert
                .Caduca = DateTimePickerCaduca.Value
                .Image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            End With
            If Await FEB.Cert.Update(_Cert, exs) Then
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al importar certificat" & Environment.NewLine)
            End If
        End If

    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
         TextBoxPwd.TextChanged,
         DateTimePickerCaduca.ValueChanged,
         Xl_Image1.AfterUpdate

        ButtonOk.Enabled = True

    End Sub
End Class