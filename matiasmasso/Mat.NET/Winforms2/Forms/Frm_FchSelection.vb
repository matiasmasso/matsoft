Public Class Frm_FchSelection
    Private _Fch As Date

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(Optional DtFch As Date = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        DateTimePicker1.Value = DtFch
    End Sub

    Public ReadOnly Property Fch As Date
        Get
            Return DateTimePicker1.Value
        End Get
    End Property

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        Dim DtFch As Date = DateTimePicker1.Value
        RaiseEvent AfterUpdate(DtFch, EventArgs.Empty)
        Me.Close()
    End Sub
End Class