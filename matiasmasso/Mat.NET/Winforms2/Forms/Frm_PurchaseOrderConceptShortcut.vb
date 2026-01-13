Public Class Frm_PurchaseOrderConceptShortcut

    Private _Value As DTOPurchaseOrder.ConceptShortcut
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPurchaseOrder.ConceptShortcut)
        MyBase.New()
        Me.InitializeComponent()
        _Value = value
    End Sub

    Private Sub Frm_Value_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrderConceptShortcut.Load(exs, _Value) Then
            With _Value
                TextBoxSearchkey.Text = .Searchkey
                Xl_LookupPdcSrc1.Value = .Src
                TextBoxEsp.Text = .Concept.Esp
                TextBoxCat.Text = .Concept.Cat
                TextBoxEng.Text = .Concept.Eng
                TextBoxPor.Text = .Concept.Por
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxSearchkey.TextChanged,
         Xl_LookupPdcSrc1.AfterUpdate,
          TextBoxEsp.TextChanged,
           TextBoxCat.TextChanged,
            TextBoxEng.TextChanged,
             TextBoxPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Value
            .Searchkey = TextBoxSearchkey.Text
            .Src = Xl_LookupPdcSrc1.Value
            .Concept.Load(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text, TextBoxPor.Text)
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.PurchaseOrderConceptShortcut.Update(exs, _Value) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.PurchaseOrderConceptShortcut.Delete(exs, _Value) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


