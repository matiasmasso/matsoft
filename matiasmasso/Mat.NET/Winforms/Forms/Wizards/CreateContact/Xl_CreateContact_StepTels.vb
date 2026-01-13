Public Class Xl_CreateContact_StepTels

    Implements IWizardStep
    Private _Contact As DTOContact
    Private _AllowEvents As Boolean

    Public Property WizardStep As Integer Implements IWizardStep.WizardStep

    ReadOnly Property Tels As List(Of DTOContactTel)
        Get
            Return Xl_Tels1.Tels
        End Get
    End Property

    ReadOnly Property Emails As List(Of DTOUser)
        Get
            Return Xl_Tels1.Emails
        End Get
    End Property

    Public Sub New(iWizardStep As Integer, oContact As DTOContact)
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        _Contact = oContact
        Xl_Tels1.Load(_Contact)
        _AllowEvents = True
    End Sub

    Private Sub ButtonTel_Click(sender As Object, e As EventArgs) Handles ButtonTel.Click
        Xl_Tels1.Do_AddNewTel()
    End Sub

    Private Sub ButtonMobile_Click(sender As Object, e As EventArgs) Handles ButtonMobile.Click
        Xl_Tels1.Do_AddNewMobil()
    End Sub

    Private Sub ButtonEmail_Click(sender As Object, e As EventArgs) Handles ButtonEmail.Click
        Xl_Tels1.Do_AddNewEmail()
    End Sub


    Private Sub AddNewEmail(Optional src As String = "")
        Dim oUser As DTOUser = DTOUser.Factory(Current.Session.Emp, _Contact, src)
        Xl_Tels1.Do_AddNewEmail(oUser)
    End Sub

    Private Sub AddNewTel(oCod As DTOContactTel.Cods, Optional src As String = "")
        Dim oTel = DTOContactTel.Factory(_Contact, oCod, src)
        Xl_Tels1.Do_AddNewTel()
    End Sub


    Private Sub Button_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles _
         ButtonTel.DragEnter,
          ButtonFax.DragEnter,
           ButtonMobile.DragEnter,
            ButtonEmail.DragEnter

        If e.Data.GetDataPresent(DataFormats.StringFormat) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ButtonTel_DragDrop(sender As Object, e As DragEventArgs) Handles ButtonTel.DragDrop
        Dim src As String = e.Data.GetData(DataFormats.StringFormat)
        If DTOBaseTel.IsPhoneNumber(src) Then
            If src.StartsWith("6") Then
                AddNewTel(DTOContactTel.Cods.movil, src)
            Else
                AddNewTel(DTOContactTel.Cods.tel, src)
            End If
        Else
            Dim sMsg As String = String.Format("{0} no sembla un número de telèfon", src)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Sub ButtonMobile_DragDrop(sender As Object, e As DragEventArgs) Handles ButtonMobile.DragDrop
        Dim src As String = e.Data.GetData(DataFormats.StringFormat)
        If DTOBaseTel.IsPhoneNumber(src) Then
            AddNewTel(DTOContactTel.Cods.movil, src)
        Else
            Dim sMsg As String = String.Format("{0} no sembla un número de mòbil", src)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub ButtonEmail_DragDrop(sender As Object, e As DragEventArgs) Handles _
        ButtonEmail.DragDrop

        Dim src As String = e.Data.GetData(DataFormats.StringFormat)
        If DTOUser.IsEmailNameAddressValid(src) Then
            AddNewEmail(src)
        Else
            Dim sMsg As String = String.Format("{0} no sembla una adreça email", src)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
        End If

    End Sub


End Class

