Public Class Frm__Wizard
    Private _Lang As DTOLang
    Private _CurrentTab As Integer
    Private _TabsCount As Integer

    Public Event requestForNextStep(sender As Object, e As MatEventArgs)
    Public Event requestForPreviousStep(sender As Object, e As MatEventArgs)
    Public Event requestToFinish(sender As Object, e As MatEventArgs)

    Public Enum ButtonIds
        Previous
        [Next]
        Finish
    End Enum

    Public Sub SetProperties(iTabsCount As Integer)
        _TabsCount = iTabsCount
        _Lang = Current.Session.Lang
    End Sub

    Private Sub Frm__Wizard_Load(sender As Object, e As EventArgs) Handles Me.Load
        ButtonPrevious.Text = _Lang.Tradueix("Anterior", "Anterior", "Previous", "Anterior")
        ButtonNext.Text = _Lang.Tradueix("Siguiente", "Següent", "Next", "Próximo")
        ButtonFinish.Text = _Lang.Tradueix("Finalizar", "Finalitzar", "Finish", "Fim")
        SetButtons()
    End Sub

    Protected Sub LoadStepControl(oControl As Control)
        PanelMain.Controls.Clear()
        PanelMain.Controls.Add(oControl)
        oControl.Dock = DockStyle.Fill
    End Sub


    Public Sub SetButtons()
        ButtonPrevious.Enabled = _CurrentTab > 0
        If _CurrentTab = _TabsCount - 1 Then
            ButtonNext.Enabled = False
            ButtonFinish.Enabled = True
        End If
    End Sub

    Public Sub EnableButton(oButtonId As ButtonIds, enable As Boolean)
        Select Case oButtonId
            Case ButtonIds.Previous
                ButtonPrevious.Enabled = enable
            Case ButtonIds.Next
                ButtonNext.Enabled = enable
            Case ButtonIds.Finish
                ButtonFinish.Enabled = enable
        End Select
    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        PushNextStep()
    End Sub

    Protected Sub PushNextStep()
        _CurrentTab += 1
        SetButtons()

        Dim oStep As IWizardStep = PanelMain.Controls(0)
        RaiseEvent requestForNextStep(Me, New MatEventArgs(oStep))
    End Sub

    Private Sub ButtonPrevious_Click(sender As Object, e As EventArgs) Handles ButtonPrevious.Click
        _CurrentTab -= 1
        SetButtons()

        Dim oStep As IWizardStep = PanelMain.Controls(0)
        RaiseEvent requestForPreviousStep(Me, New MatEventArgs(oStep))
    End Sub
    Private Sub ButtonFinish_Click(sender As Object, e As EventArgs) Handles ButtonFinish.Click
        RaiseEvent requestToFinish(Me, MatEventArgs.Empty)
    End Sub

    Protected Sub SetFinishButtons(finish As Boolean)
        ButtonFinish.Enabled = finish
        ButtonNext.Enabled = Not finish
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


End Class