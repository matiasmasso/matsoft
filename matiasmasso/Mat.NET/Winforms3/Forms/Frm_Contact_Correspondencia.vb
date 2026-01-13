

Public Class Frm_Contact_Correspondencia
    Private _Contact As DTOContact

    Private _AllowEvents As Boolean

    Private Enum ColsMails
        Guid
        Id
        Fch
        Mime
        Ico
        RT
        Subject
        Usr
    End Enum

    Public Sub New(oContact As DTOContact)
        MyBase.New
        InitializeComponent()
        _Contact = oContact
    End Sub

    Private Async Sub Frm_Contact_Correspondencia_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        FEB.Contact.Load(_Contact, exs)
        Me.Text = "Correspondencia i memos de " & _Contact.FullNom
        If Await RefrescaCorrespondencia(exs) Then
            RefrescaMemos()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub RefrescaCorrespondencia() 'dels AddHandler
        Dim exs As New List(Of Exception)
        If Await RefrescaCorrespondencia(exs) Then
            RefrescaMemos()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function RefrescaCorrespondencia(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oCorrespondencies = Await FEB.Correspondencias.All(_Contact, exs)
        If exs.Count = 0 Then
            Xl_Contact_Correspondencies1.Load(oCorrespondencies)
            retval = True
        End If
        Return retval
    End Function

    Private Async Sub RefrescaMemos()
        Dim exs As New List(Of Exception)
        Dim oMems = Await FEB.Mems.All(exs, oContact:=_Contact)
        If exs.Count = 0 Then
            Xl_Mems1.Load(oMems)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ShowMail(ByVal oMail As DTOCorrespondencia)
        Dim oFrm As New Frm_Correspondencia(oMail)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaCorrespondencia
        oFrm.Show()
    End Sub

    Private Async Sub ToolStripButtonRefrescaMails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await RefrescaCorrespondencia(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ToolStripButtonRefrescaMemos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefrescaMemos()
    End Sub

    Private Sub ToolStripButtonNewMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oMail = DTOCorrespondencia.Factory(Current.Session.User, _Contact, DTOCorrespondencia.Cods.Rebut)
        ShowMail(oMail)
    End Sub

    Private Sub ToolStripButtonNewMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMem As New DTOMem()
        With oMem
            .Contact = _Contact.ToGuidNom()
            .Fch = DTO.GlobalVariables.Today()
            .Cod = DTOMem.Cods.Despaitx
            .UsrLog = DTOUsrLog2.Factory(Current.Session.User)
        End With

        Dim oFrm As New Frm_Mem(oMem)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaMemos
        oFrm.Show()
    End Sub


    Private Async Sub Xl_Contact_Correspondencies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Correspondencies1.RequestToRefresh
        Dim exs As New List(Of Exception)
        If Not Await RefrescaCorrespondencia(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Mems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToAddNew
        Dim oMem As New DTOMem
        With oMem
            .Fch = DTO.GlobalVariables.Today()
            .UsrLog = DTOUsrLog2.Factory(Current.Session.User)
            .Contact = _Contact.ToGuidNom()
        End With
        Dim oFrm As New Frm_Mem(oMem)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaMemos
        oFrm.Show()
    End Sub

    Private Sub Xl_Mems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToRefresh
        RefrescaMemos()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Mems1.Filter = e.Argument
        Xl_Contact_Correspondencies1.Filter = e.Argument
    End Sub

    Private Sub Xl_ContactCorrespondencies1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contact_Correspondencies1.RequestToAddNew
        Dim oCorrespondencia = DTOCorrespondencia.Factory(Current.Session.User, _Contact, DTOCorrespondencia.Cods.Rebut)
        Dim oFrm As New Frm_Correspondencia(oCorrespondencia)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaCorrespondencia
        oFrm.Show()

    End Sub


End Class


