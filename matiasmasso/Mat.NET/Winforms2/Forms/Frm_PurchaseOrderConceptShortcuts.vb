Public Class Frm_PurchaseOrderConceptShortcuts
    Private _DefaultValue As DTOPurchaseOrder.ConceptShortcut
    Private _Lang As DTOLang
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oLang As DTOLang, Optional oDefaultValue As DTOPurchaseOrder.ConceptShortcut = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _Lang = oLang
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Templates_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_PurchaseOrderConceptShortcuts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderConceptShortcuts1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_PurchaseOrderConceptShortcuts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderConceptShortcuts1.RequestToAddNew
        Dim value As New DTOPurchaseOrder.ConceptShortcut
        Dim oFrm As New Frm_PurchaseOrderConceptShortcut(value)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PurchaseOrderConceptShortcuts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderConceptShortcuts1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim values = Await FEB.PurchaseOrderConceptShortcuts.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_PurchaseOrderConceptShortcuts1.Load(values, _Lang, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class