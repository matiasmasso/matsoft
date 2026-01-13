Public Class Frm_MatsoftSupportCase

    Private _value As DTOMatsoftSupportCase
    Private _isNew As Boolean
    Private _allowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOMatsoftSupportCase, Optional isNew As Boolean = False)
        MyBase.New
        InitializeComponent()
        _value = value
        _isNew = isNew
    End Sub

    Private Sub refresca()
        With _value
            LabelFch.Text = Format(.FchOpen, "dd/MM/yy HH:mm")
            Xl_Screenshot1.image = LegacyHelper.ImageHelper.Converter(.Screenshot)
            TextBoxDsc.Text = .Dsc
            TextBoxAnswer.Text = .Answer
        End With
    End Sub

    Private Sub Frm_MatsoftSupportCase_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _isNew Then
            LabelFch.Visible = False
            LabelFchValue.Visible = False
            _value.FchOpen = Now
            refresca()
        Else
            Dim exs As New List(Of Exception)
            If FEB2.MatsoftSupportCase.Load(_value, exs) Then
                refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
        _allowEvents = True
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _value
            .Screenshot = LegacyHelper.ImageHelper.Converter(Xl_Screenshot1.image)
            .Dsc = TextBoxDsc.Text
            .Answer = TextBoxAnswer.Text
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.MatsoftSupportCase.Update(_value, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.MatsoftSupportCase.Delete(_value, exs) Then
            Dim sMessage As String = String.Format("Incidencia software de {0} eliminada", DTOUser.NicknameOrElse(Current.Session.User))
            If Await FEB2.UserTaskId.Log(DTOUserTaskId.Ids.CheckSoftwareIncidence, sMessage, _value, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "No hem pogut avisar a l'informatic:")
            End If
        Else
            UIHelper.WarnError(exs, "No hem pogut desar la incidéncia:")
        End If
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
             TextBoxDsc.TextChanged,
              Xl_Screenshot1.AfterUpdate,
               ComboBoxProduct.SelectedIndexChanged

        ButtonOk.Enabled = True
    End Sub


End Class