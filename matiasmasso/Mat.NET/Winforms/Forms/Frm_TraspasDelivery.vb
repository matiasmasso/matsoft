Public Class Frm_TraspasDelivery

    Private _Value As DTOTraspasDelivery

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New
        InitializeComponent()
        _Value = BLLTraspasDelivery.Find(oDelivery.Guid)

        TextBoxHeader.Text = HeaderText()
        Xl_TraspasDeliveryItems1.Load(_Value.Items)
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_TraspasDeliveryItems1.Filter = e.Argument
    End Sub

    Private Function HeaderText() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine(String.Format("albará {0} del {1:dd/MM/yy}", _Value.Id, _Value.Fch))
        sb.AppendLine("des del magatzem " & _Value.MgzFrom.Abr)
        sb.AppendLine("cap el magatzem " & _Value.MgzTo.Abr)

        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class