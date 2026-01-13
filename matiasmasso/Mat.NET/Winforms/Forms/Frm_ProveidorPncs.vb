Public Class Frm_ProveidorPncs
    Private _Proveidor As DTOContact
    Private _Values As List(Of DTOPurchaseOrderItem)

    Public Sub New(oProveidor As DTOContact)
        MyBase.New
        InitializeComponent()
        _Proveidor = oProveidor
    End Sub

    Public Sub New(items As List(Of DTOPurchaseOrderItem))
        MyBase.New
        InitializeComponent()
        _Values = items
        _Proveidor = _Values.First.PurchaseOrder.contact
        Xl_ProveidorPncs1.Load(_Values, _Proveidor.lang)
    End Sub

    Private Async Sub Frm_ProveidorPncs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Proveidor, exs) Then
            Me.Text = String.Format("Comandes pendents de {0}", _Proveidor.FullNom)
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        If _Values Is Nothing Then
            ProgressBar1.Visible = True
            _Values = Await FEB2.PurchaseOrderItems.Pending(exs, _Proveidor, DTOPurchaseOrder.Codis.proveidor, GlobalVariables.Emp.Mgz)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                Dim oDisplaymode As Xl_ProveidorPncs.DisplayModes = IIf(AgruparMenuItem.Checked, Xl_ProveidorPncs.DisplayModes.Group, Xl_ProveidorPncs.DisplayModes.Table)
                Xl_ProveidorPncs1.Load(_Values, _Proveidor.lang, oDisplaymode)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            Dim oDisplaymode As Xl_ProveidorPncs.DisplayModes = IIf(AgruparMenuItem.Checked, Xl_ProveidorPncs.DisplayModes.Group, Xl_ProveidorPncs.DisplayModes.Table)
            Xl_ProveidorPncs1.Load(_Values, _Proveidor.lang, oDisplaymode)
        End If
    End Function

    Private Async Sub Xl_ProveidorPncs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProveidorPncs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_ProveidorPncs1.Filter = e.Argument
    End Sub

    Private Sub AgruparMenuItem_Click(sender As Object, e As EventArgs) Handles AgruparMenuItem.Click
        Dim oDisplaymode As Xl_ProveidorPncs.DisplayModes = IIf(AgruparMenuItem.Checked, Xl_ProveidorPncs.DisplayModes.Group, Xl_ProveidorPncs.DisplayModes.Table)
        Xl_ProveidorPncs1.Load(_Values, _Proveidor.lang, oDisplaymode)
    End Sub

    Private Sub Do_Excel()
        Dim sContactNom As String = _Proveidor.FullNom
        Dim sFilename As String = Current.Session.Lang.Tradueix("M+O Pedidos pendientes de entrega", "M+O Comandes pendents de entrega", "M+O Open orders") & " " & sContactNom & ".xlsx"
        Dim sTitle As String = Current.Session.Lang.Tradueix("M+O Pedidos pendientes de entrega", "M+O Comandes pendents de entrega", "M+O Open orders") & " " & sContactNom
        Dim oSheet = FEB2.PurchaseOrderItems.Excel(_Values, sTitle, sFilename)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ProveidorPncs1_RequestToExcel(sender As Object, e As MatEventArgs) Handles Xl_ProveidorPncs1.RequestToExcel
        Do_Excel()
    End Sub

    Private Sub ExcelMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelMenuItem.Click
        Do_Excel()
    End Sub


End Class