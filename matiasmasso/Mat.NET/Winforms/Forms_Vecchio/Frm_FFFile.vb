Public Class Frm_FFFile
    Private _FFFile As FFFile

    Public Sub New(value As FFFile)
        MyBase.New()
        Me.InitializeComponent()
        _FFFile = value
    End Sub

    Private Sub Frm_FFFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_FFRegistres1.Load(_FFFile.Registres)
    End Sub

    Private Sub Xl_FFRegistres1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_FFRegistres1.ValueChanged
        Dim oFFRegistre As FFRegistre = e.Argument
        Xl_FFFields1.Load(oFFRegistre.Fields)
    End Sub
End Class