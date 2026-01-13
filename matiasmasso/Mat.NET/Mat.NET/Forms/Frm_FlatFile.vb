Public Class Frm_FlatFile
    Private _FlatFile As DTOFlatFile

    Public Sub New(oFlatFile As DTOFlatFile)
        MyBase.New()
        Me.InitializeComponent()
        _FlatFile = oFlatFile
        Xl_FlatRegs1.Load(oFlatFile)
    End Sub

    Private Sub Xl_FlatRegs1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_FlatRegs1.ValueChanged
        Dim oReg As DTOFlatReg = e.Argument
        Xl_FlatFields1.Load(oReg.Fields)
    End Sub
End Class