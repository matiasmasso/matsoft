Public Class Frm_Budget

    Private _Budget As DTOBudget
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event AfterDelete(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Partides
        Rols
        Downloads
    End Enum

    Public Sub New(value As DTOBudget)
        MyBase.New()
        Me.InitializeComponent()
        _Budget = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Budget.Load(_Budget, exs) Then
            With _Budget
                NumericUpDown1.Value = .Exercici.Year
                If .Parent IsNot Nothing Then NumericUpDown1.Enabled = False
                TextBoxNom.Text = .Nom
                Xl_Contact21.Contact = .Contact
                Xl_DocFile1.Load(.Docfile)
                Xl_BudgetItems1.Load(.Items)

                Dim oRols = Await FEB2.Rols.All(exs)
                If exs.Count = 0 Then
                    Xl_RolsAllowed1.Load(oRols, .Rols)
                Else
                    UIHelper.WarnError(exs)
                End If

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         NumericUpDown1.ValueChanged,
          Xl_Contact21.AfterUpdate,
           Xl_RolsAllowed1.AfterUpdate,
            Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Budget
            .Exercici = DTOExercici.FromYear(Current.Session.Emp, NumericUpDown1.Value)
            .nom = TextBoxNom.Text
            .Contact = Xl_Contact21.Contact
            If Xl_DocFile1.IsDirty Then
                .Docfile = Xl_DocFile1.Value
            End If
            .Rols = Xl_RolsAllowed1.CheckedValues
            .Items = Xl_BudgetItems1.Items
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Budget.Update(_Budget, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Budget))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Budget.Delete(_Budget, exs) Then
                RaiseEvent AfterDelete(Me, New MatEventArgs(_Budget))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub



    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Partides
                Static donePartides As Boolean
                If Not donePartides Then
                    refrescaPartides()
                    donePartides = True
                End If
            Case Tabs.Downloads
                Static done As Boolean
                If Not done Then
                    refrescaDocfiles()
                    done = True
                End If
        End Select
    End Sub


#Region "Partides"
    Private Sub refrescaPartides()
        Dim oPartides As List(Of DTOBudget) = DTOBudget.Partidas(_Budget)
        Dim items As List(Of DTOBudget.Item) = oPartides.SelectMany(Function(x) x.Items).ToList
        Xl_BudgetItems1.Load(items)
    End Sub

    Private Sub Xl_BudgetItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BudgetItems1.RequestToRefresh
        refrescaPartides()
    End Sub

    Private Sub refrescaItems()
        'Dim items As List(Of DTOBudget.Item) = BLLBudgetItems.All(_Budget)
        'Xl_BudgetItems1.Load(items)
    End Sub

#End Region

#Region "DocfileSrcs"
    Private Sub Xl_DocfileSrcs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToAddNew
        Dim oDocfileSrc As New DTODocFileSrc()
        With oDocfileSrc
            .Cod = DTODocFile.Cods.Download
            .Src = _Budget
        End With
        Dim oFrm As New Frm_DocfileSrc(oDocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaDocfiles
        oFrm.Show()
    End Sub

    Private Async Sub refrescaDocfiles()
        Dim exs As New List(Of Exception)
        Dim oDocFileSrcs = Await FEB2.DocFileSrcs.All(_Budget, exs)
        If exs.Count = 0 Then
            Xl_DocfileSrcs1.Load(oDocFileSrcs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToRefresh
        refrescaDocfiles()
    End Sub
#End Region

End Class


