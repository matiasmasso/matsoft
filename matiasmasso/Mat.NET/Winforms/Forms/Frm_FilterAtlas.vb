Imports Winforms

Public Class Frm_FilterAtlas
    Private _AllItems As IEnumerable(Of DTOContact)
    Private _SelectedItems As IEnumerable(Of DTOContact)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oAllItems As IEnumerable(Of DTOContact), oSelectedItems As IEnumerable(Of DTOContact))
        InitializeComponent()
        _AllItems = oAllItems
        _SelectedItems = oSelectedItems
    End Sub

    Private Sub Frm_FilterAtlas_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_TreeFilterAtlas1.load(_AllItems, _SelectedItems)
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim retval = Xl_TreeFilterAtlas1.SelectedItems
        RaiseEvent AfterUpdate(Me, New MatEventArgs(retval))
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_TreeFilterAtlas1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TreeFilterAtlas1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub
End Class