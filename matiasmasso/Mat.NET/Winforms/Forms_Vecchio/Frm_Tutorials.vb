Public Class Frm_Tutorials

    Private _Parent As DTOTutorialSubject

    Public Sub New(oParent As DTOTutorialSubject)
        MyBase.New
        InitializeComponent()

        _Parent = oParent

    End Sub

    Private Sub Frm_Tutorials_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub


    Private Sub Xl_Tutorials1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Tutorials1.RequestToAddNew
        Dim oTutorial As New DTOTutorial
        oTutorial.Fch = Today
        oTutorial.Parent = _Parent
        Dim oFrm As New Frm_Tutorial(oTutorial)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Tutorials1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Tutorials1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oTutorials As List(Of DTOTutorial) = BLL.BLLTutorials.All(_Parent)
        Xl_Tutorials1.Load(oTutorials)
    End Sub
End Class