Public Class Frm_TutorialSubjects

    Public Sub New()
        MyBase.New()
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_TutorialSubjects_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_TutorialSubjects1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_TutorialSubjects1.RequestToAddNew
        Dim oTutorialSubject As New DTOTutorialSubject
        Dim oFrm As New Frm_TutorialSubject(oTutorialSubject)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_TutorialSubjects1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_TutorialSubjects1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oTutorialSubjects As List(Of DTOTutorialSubject) = BLL.BLLTutorialSubjects.All
        Xl_TutorialSubjects1.Load(oTutorialSubjects)
    End Sub
End Class