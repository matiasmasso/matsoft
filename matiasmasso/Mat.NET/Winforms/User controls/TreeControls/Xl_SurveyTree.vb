Public Class Xl_SurveyTree
    Inherits TreeView

    Private _Survey As DTOSurvey
    Private _Participant As DTOSurveyParticipant
    Private _Mode As Modes = Modes.Survey

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event afterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Modes
        Survey
        Participant
    End Enum
    Private Enum levels
        NotSet
        survey
        [step]
        question
        answer
    End Enum

    Public Shadows Sub Load(oSurvey As DTOSurvey, oScores As List(Of DTOSurveyAnswer))
        SetProperties()

        _Survey = oSurvey

        MyBase.Nodes.Clear()
        Dim oRoot As TreeNode = MyBase.Nodes.Add(oSurvey.Title)
        oRoot.Tag = oSurvey

        For Each oStep As DTOSurveyStep In oSurvey.Steps
            Dim sStepCaption As String = BLLSurveyStep.CaptionWithScores(oStep, oScores)
            Dim oStepNode As TreeNode = oRoot.Nodes.Add(sStepCaption)
            oStepNode.Tag = oStep

            For Each oQuestion As DTOSurveyQuestion In oStep.Questions
                Dim oQuestionNode As TreeNode = oStepNode.Nodes.Add(oQuestion.Text)
                oQuestionNode.Tag = oQuestion

                For Each oAnswer As DTOSurveyAnswer In oQuestion.Answers
                    Dim sCaption As String = oAnswer.Text
                    If _Mode = Modes.Survey Then sCaption = BLLSurveyAnswer.CaptionWithScores(oAnswer, oScores)
                    Dim oAnswerNode As TreeNode = oQuestionNode.Nodes.Add(sCaption)

                    oAnswerNode.Tag = oAnswer
                    If _Mode = Modes.Participant Then
                        If BLLSurveyParticipant.IsAnswered(_Participant, oAnswer) Then
                            oAnswerNode.ForeColor = Color.Red
                        End If
                    End If
                Next
                oQuestionNode.Expand()
            Next
            oStepNode.Expand()
        Next
        oRoot.Expand()

    End Sub

    Public Shadows Sub Load(oParticipant As DTOSurveyParticipant)
        _Participant = oParticipant
        _Mode = Modes.Participant
        Load(_Participant.Survey, Nothing)
    End Sub

    Private Sub SetProperties()
        Static propertiesSet As Boolean
        If Not propertiesSet Then
            MyBase.LabelEdit = True
            propertiesSet = True
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oMenu As New ContextMenuStrip

        Dim oNode As TreeNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            Select Case Level(oNode)
                Case levels.survey
                    oMenu.Items.Add(New ToolStripMenuItem("editar", Nothing, AddressOf Do_EditSurvey))
                    oMenu.Items.Add(New ToolStripMenuItem("afegir pas", Nothing, AddressOf AddNewStep))
                Case levels.step
                    Dim item As DTOSurveyStep = oNode.Tag
                    If item IsNot Nothing Then
                        Dim oMenuRange As New Menu_SurveyStep(item)
                        AddHandler oMenuRange.AfterUpdate, AddressOf RefreshRequest
                        oMenu.Items.AddRange(oMenuRange.Range)
                    End If
                    oMenu.Items.Add("-")
                    oMenu.Items.Add(New ToolStripMenuItem("afegir pregunta", Nothing, AddressOf AddNewQuestion))
                Case levels.question
                    Dim item As DTOSurveyQuestion = oNode.Tag
                    If item IsNot Nothing Then
                        Dim oMenuRange As New Menu_SurveyQuestion(item)
                        AddHandler oMenuRange.AfterUpdate, AddressOf RefreshRequest
                        oMenu.Items.AddRange(oMenuRange.Range)
                    End If
                    oMenu.Items.Add("-")
                    oMenu.Items.Add(New ToolStripMenuItem("afegir resposta", Nothing, AddressOf AddNewAnswer))
                Case levels.answer
                    Dim item As DTOSurveyAnswer = oNode.Tag
                    If item IsNot Nothing Then
                        Dim oMenuRange As New Menu_SurveyAnswer(item)
                        AddHandler oMenuRange.AfterUpdate, AddressOf RefreshRequest
                        oMenu.Items.AddRange(oMenuRange.Range)
                    End If
            End Select
        End If
        MyBase.ContextMenuStrip = oMenu
    End Sub

    Private Sub Do_EditSurvey(sender As Object, e As EventArgs)
        Dim oNode As TreeNode = MyBase.SelectedNode
        Dim oSurvey As DTOSurvey = oNode.Tag
        Dim oFrm As New Frm_Survey(oSurvey)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub AddNewStep(sender As Object, e As EventArgs)
        Dim oParentNode As TreeNode = MyBase.SelectedNode
        Dim oTag As New DTOSurveyStep()
        With oTag
            .Parent = oParentNode.Tag
            .Text = "(nou pas d'enquesta)"
            .Ord = oParentNode.Nodes.Count
        End With
        Dim exs As New List(Of Exception)
        If BLLSurveyStep.Update(oTag, exs) Then
            Dim oChildNode As TreeNode = oParentNode.Nodes.Add(oTag.Text)
            oChildNode.Tag = oTag
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub AddNewQuestion(sender As Object, e As EventArgs)
        Dim oParentNode As TreeNode = MyBase.SelectedNode
        Dim oTag As New DTOSurveyQuestion
        With oTag
            .Parent = oParentNode.Tag
            .Text = "(nova pregunta)"
            .Ord = oParentNode.Nodes.Count
        End With
        Dim exs As New List(Of Exception)
        If BLLSurveyQuestion.Update(oTag, exs) Then
            Dim oChildNode As TreeNode = oParentNode.Nodes.Add(oTag.Text)
            oChildNode.Tag = oTag
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub AddNewAnswer(sender As Object, e As EventArgs)
        Dim oParentNode As TreeNode = MyBase.SelectedNode
        Dim oTag As New DTOSurveyAnswer()
        With oTag
            .Parent = oParentNode.Tag
            .Text = "(nova resposta)"
            .Ord = oParentNode.Nodes.Count
        End With
        Dim exs As New List(Of Exception)
        If BLLSurveyAnswer.Update(oTag, exs) Then
            Dim oChildNode As TreeNode = oParentNode.Nodes.Add(oTag.Text)
            oChildNode.Tag = oTag
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub TreeNode_NodeMouseDoubleClick() Handles MyBase.NodeMouseDoubleClick
        Dim oNode As TreeNode = MyBase.SelectedNode
        Select Case Level(oNode)
            Case levels.survey
                Do_EditSurvey(Me, EventArgs.Empty)
            Case levels.step
                Dim oFrm As New Frm_SurveyStep(oNode.Tag)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case levels.question
                Dim oFrm As New Frm_SurveyQuestion(oNode.Tag)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case levels.answer
                Dim oFrm As New Frm_SurveyAnswer(oNode.Tag)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub Xl_WinMenuTree_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_SurveyTree_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles Me.AfterLabelEdit
        Dim exs As New List(Of Exception)
        Select Case Level(e.Node)
            Case levels.survey
                Dim item As DTOSurvey = e.Node.Tag
                item.Title = e.Label
                If BLLSurvey.Update(item, exs) Then
                    RaiseEvent afterUpdate(Me, New MatEventArgs(item))
                Else
                    UIHelper.WarnError(exs)
                End If
            Case levels.step
                Dim item As DTOSurveyStep = e.Node.Tag
                BLLSurveyStep.Load(item)
                item.Title = e.Label
                If BLLSurveyStep.Update(item, exs) Then
                    RaiseEvent afterUpdate(Me, New MatEventArgs(item))
                Else
                    UIHelper.WarnError(exs)
                End If
            Case levels.question
                Dim item As DTOSurveyQuestion = e.Node.Tag
                BLLSurveyQuestion.Load(item)
                item.Text = e.Label
                If BLLSurveyQuestion.Update(item, exs) Then
                    RaiseEvent afterUpdate(Me, New MatEventArgs(item))
                Else
                    UIHelper.WarnError(exs)
                End If
            Case levels.answer
                Dim item As DTOSurveyAnswer = e.Node.Tag
                BLLSurveyAnswer.Load(item)
                item.Text = e.Label
                If BLLSurveyAnswer.Update(item, exs) Then
                    RaiseEvent afterUpdate(Me, New MatEventArgs(item))
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select
    End Sub




    Private Function Level(oNode As TreeNode) As levels
        Dim retval As levels = levels.NotSet
        If TypeOf oNode.Tag Is DTOSurvey Then
            retval = levels.survey
        ElseIf TypeOf oNode.Tag Is DTOSurveyStep Then
            retval = levels.step
        ElseIf TypeOf oNode.Tag Is DTOSurveyQuestion Then
            retval = levels.question
        ElseIf TypeOf oNode.Tag Is DTOSurveyAnswer Then
            retval = levels.answer
        End If
        Return retval
    End Function
End Class

