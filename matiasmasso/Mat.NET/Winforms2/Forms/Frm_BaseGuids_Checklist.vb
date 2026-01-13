Public Class Frm_BaseGuids_Checklist
    Private _AllValues As IEnumerable(Of DTOBaseGuid)
    Private _SelectedValues As IEnumerable(Of DTOBaseGuid)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oAllValues As IEnumerable(Of DTOBaseGuid), oSelectedValues As IEnumerable(Of DTOBaseGuid))
        MyBase.New
        InitializeComponent()
        _AllValues = oAllValues
        _SelectedValues = oSelectedValues
    End Sub

    Private Sub Frm_Reps_Checklist_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_BaseGuids_Checklist1.Load(_AllValues, _SelectedValues)

    End Sub

    Private Sub Xl_Reps_Checklist1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_BaseGuids_Checklist1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Xl_BaseGuids_Checklist1.CheckedValues))
        Me.Close()
    End Sub
End Class