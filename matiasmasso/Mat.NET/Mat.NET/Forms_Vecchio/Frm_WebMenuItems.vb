Public Class Frm_WebMenuItems
    Private mSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event AfterSelect(sender As Object, e As System.EventArgs)

    Public Sub New(oSelectionMode As BLL.Defaults.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_WebMenuItems_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Xl_WebMenuItems1.LoadAllItems(mSelectionMode)
    End Sub

    Private Sub Xl_WebMenuItems1_AfterSelect(sender As Object, e As System.EventArgs) Handles Xl_WebMenuItems1.AfterSelect
        RaiseEvent AfterSelect(sender, e)
    End Sub
End Class