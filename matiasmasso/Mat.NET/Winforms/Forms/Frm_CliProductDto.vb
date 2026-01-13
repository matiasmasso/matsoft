Public Class Frm_CliProductDto
    Private _Value As DTOCliProductDto
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCliProductDto)
        MyBase.New
        InitializeComponent()
        _Value = value
        With _Value
            Xl_Contact21.Contact = .Customer
            Xl_LookupProduct1.Product = .Product
            Xl_Percent1.Value = .Dto
        End With
        _AllowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Value
            .Product = Xl_LookupProduct1.Product
            .Dto = Xl_Percent1.Value
        End With

        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
        Me.Close()
    End Sub

    Private Sub ControlChanged(sender As Object, e As MatEventArgs) Handles _
        Xl_LookupProduct1.AfterUpdate,
         Xl_Percent1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub
End Class