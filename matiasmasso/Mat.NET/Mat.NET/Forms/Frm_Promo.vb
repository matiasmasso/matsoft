Public Class Frm_Promo

    Private _Promo As DTOPromo
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPromo)
        MyBase.New()
        Me.InitializeComponent()
        _Promo = value
        BLL.BLLPromo.Load(_Promo)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Promo
            TextBoxCaption.Text = .Caption
            If .FchFrom >= DateTimePickerFchFrom.MinDate Then
                DateTimePickerFchFrom.Value = .FchFrom
            End If
            If .FchTo >= DateTimePickerFchTo.MinDate Then
                DateTimePickerFchTo.Value = .FchTo
            End If
            If .Product IsNot Nothing Then
                Xl_LookupProduct1.Product = .Product
            End If
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxCaption.TextChanged, _
         Xl_LookupProduct1.AfterUpdate, _
          DateTimePickerFchFrom.ValueChanged, _
           DateTimePickerFchTo.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Promo
            .Caption = TextBoxCaption.Text
            .Product = Xl_LookupProduct1.Product
            .FchFrom = DateTimePickerFchFrom.Value
            .FchTo = DateTimePickerFchTo.Value
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLPromo.Update(_Promo, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Promo))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLPromo.Delete(_Promo, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Promo))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


