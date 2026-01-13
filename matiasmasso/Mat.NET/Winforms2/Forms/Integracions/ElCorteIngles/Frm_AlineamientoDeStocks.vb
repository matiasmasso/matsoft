Public Class Frm_AlineamientoDeStocks
    Private _value As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad

    Public Sub New(value As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        MyBase.New
        InitializeComponent()
        _value = value
    End Sub

    Private Async Sub Frm_AlineamientoDeStocks_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _value = Await FEB.ElCorteIngles.AlineamientoDeDisponibilidad(exs, _value.Guid, Current.Session.User)
        If exs.Count = 0 Then
            Xl_ElCorteIngles_AlineamientoDisponibilidad1.Load(_value.Items)
            RichTextBox1.Text = _value.Text
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ArxiuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArxiuToolStripMenuItem.Click
        Dim oDlg As New SaveFileDialog()
        With oDlg
            .DefaultExt = "Csv"
            .Title = "Desar fitxer Csv de alineament stocks Eci"
            .Filter = "fitxers Csv|*.Csv|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                System.IO.File.WriteAllText(.FileName, _value.Text)
            End If
        End With
    End Sub
End Class

