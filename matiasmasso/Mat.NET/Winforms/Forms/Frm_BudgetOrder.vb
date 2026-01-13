Public Class Frm_BudgetOrder
    Private _BudgetOrder As DTOBudgetOrder
    Private _done As Boolean

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        BudgetItems
        BookFras
    End Enum

    Public Sub New(value As DTOBudgetOrder)
        MyBase.New()
        Me.InitializeComponent()
        _BudgetOrder = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.BudgetOrder.Load(_BudgetOrder, exs) Then
            With _BudgetOrder
                TextBoxNum.Text = .Num
                DateTimePicker1.Value = .Fch
                Xl_Contact21.Contact = .Contact
                Xl_Amount1.Amt = .Amt
                TextBoxObs.Text = .Obs
                Xl_DocFile1.Load(.Docfile)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNum.TextChanged,
         DateTimePicker1.ValueChanged,
          Xl_Contact21.AfterUpdate,
           Xl_Amount1.AfterUpdate,
            TextBoxObs.TextChanged,
             Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _BudgetOrder
            .Num = TextBoxNum.Text
            .Fch = DateTimePicker1.Value
            .Contact = Xl_Contact21.Contact
            .Amt = Xl_Amount1.Amt
            .Obs = TextBoxObs.Text
            If Xl_DocFile1.IsDirty Then
                .Docfile = Xl_DocFile1.Value
            End If
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.BudgetOrder.Update(_BudgetOrder, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BudgetOrder))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
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
            If Await FEB2.BudgetOrder.Delete(_BudgetOrder, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BudgetOrder))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.BookFras
                If Not _done Then
                    Dim exs As New List(Of Exception)
                    Dim oBudgetOrderFras = Await FEB2.BudgetOrderFras.All(exs, _BudgetOrder)
                    Dim oBookFras As List(Of DTOBookFra) = oBudgetOrderFras.Select(Function(x) x.BookFra).ToList
                    Xl_BookfrasCompact1.Load(oBookFras)
                    _done = True
                End If
        End Select
    End Sub
End Class


