

Public Class Frm_Doc_Select
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ComboBoxYea As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxNum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelNum As System.Windows.Forms.Label
    Friend WithEvents ButtonZoom As System.Windows.Forms.Button
    Friend WithEvents ButtonFrx As System.Windows.Forms.Button
    Friend WithEvents ButtonCopyLink As System.Windows.Forms.Button
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Doc_Select))
        Me.ComboBoxYea = New System.Windows.Forms.ComboBox()
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelNum = New System.Windows.Forms.Label()
        Me.ButtonZoom = New System.Windows.Forms.Button()
        Me.ButtonFrx = New System.Windows.Forms.Button()
        Me.ButtonCopyLink = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ComboBoxYea
        '
        Me.ComboBoxYea.DropDownWidth = 72
        Me.ComboBoxYea.Location = New System.Drawing.Point(16, 24)
        Me.ComboBoxYea.Name = "ComboBoxYea"
        Me.ComboBoxYea.Size = New System.Drawing.Size(80, 21)
        Me.ComboBoxYea.TabIndex = 1
        Me.ComboBoxYea.TabStop = False
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        Me.TextBoxNum.ContextMenu = Me.ContextMenu1
        Me.TextBoxNum.Location = New System.Drawing.Point(16, 72)
        Me.TextBoxNum.Multiline = True
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(80, 119)
        Me.TextBoxNum.TabIndex = 3
        '
        'ContextMenu1
        '
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "any:"
        '
        'LabelNum
        '
        Me.LabelNum.Location = New System.Drawing.Point(16, 56)
        Me.LabelNum.Name = "LabelNum"
        Me.LabelNum.Size = New System.Drawing.Size(56, 16)
        Me.LabelNum.TabIndex = 2
        Me.LabelNum.Text = "números:"
        '
        'ButtonZoom
        '
        Me.ButtonZoom.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.ButtonZoom.Enabled = False
        Me.ButtonZoom.Location = New System.Drawing.Point(102, 72)
        Me.ButtonZoom.Name = "ButtonZoom"
        Me.ButtonZoom.Size = New System.Drawing.Size(104, 40)
        Me.ButtonZoom.TabIndex = 4
        Me.ButtonZoom.Text = "ZOOM"
        '
        'ButtonFrx
        '
        Me.ButtonFrx.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.ButtonFrx.Enabled = False
        Me.ButtonFrx.Image = DirectCast(resources.GetObject("ButtonFrx.Image"), System.Drawing.Image)
        Me.ButtonFrx.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonFrx.Location = New System.Drawing.Point(102, 152)
        Me.ButtonFrx.Name = "ButtonFrx"
        Me.ButtonFrx.Size = New System.Drawing.Size(104, 40)
        Me.ButtonFrx.TabIndex = 6
        Me.ButtonFrx.Text = "FACTURAR"
        Me.ButtonFrx.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ButtonCopyLink
        '
        Me.ButtonCopyLink.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.ButtonCopyLink.Enabled = False
        Me.ButtonCopyLink.Image = Global.Winforms.My.Resources.Resources.Copy
        Me.ButtonCopyLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCopyLink.Location = New System.Drawing.Point(102, 112)
        Me.ButtonCopyLink.Name = "ButtonCopyLink"
        Me.ButtonCopyLink.Size = New System.Drawing.Size(104, 40)
        Me.ButtonCopyLink.TabIndex = 5
        Me.ButtonCopyLink.Text = "COPIAR LINK"
        Me.ButtonCopyLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Frm_Doc_Select
        '
        Me.ClientSize = New System.Drawing.Size(216, 195)
        Me.Controls.Add(Me.ButtonCopyLink)
        Me.Controls.Add(Me.ButtonFrx)
        Me.Controls.Add(Me.ButtonZoom)
        Me.Controls.Add(Me.ComboBoxYea)
        Me.Controls.Add(Me.TextBoxNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LabelNum)
        Me.Name = "Frm_Doc_Select"
        Me.Text = "SELECCIONAR DOCUMENTS.."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Enum Styles
        Comanda
        Albara
        Factura
        Assentament
        Incidencia
    End Enum

    Private mStyle As Styles
    Private mEmp As DTOEmp = Current.session.emp


    Private Sub Frm_Doc_Select_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadYeas()
    End Sub


    Public WriteOnly Property Style() As Styles
        Set(ByVal Value As Styles)
            mStyle = Value
            Select Case mStyle
                Case Styles.Comanda
                    LabelNum.Text = "Comanda:"
                Case Styles.Albara
                    LabelNum.Text = "Albarà:"
                Case Styles.Factura
                    LabelNum.Text = "Factura:"
                Case Styles.Assentament
                    LabelNum.Text = "Assentament:"
                Case Styles.Incidencia
                    LabelNum.Text = "Incidencia:"
            End Select
        End Set
    End Property

    Private Async Function Incidencias() As Task(Of List(Of DTOIncidencia))
        Dim exs As New List(Of Exception)
        Dim itm As String
        Dim retval As New List(Of DTOIncidencia)
        For Each itm In NumList()
            Dim oIncidencia = Await FEB2.Incidencia.FromNum(exs, GlobalVariables.Emp, itm)
            retval.Add(oIncidencia)
        Next
        Return retval
    End Function

    Private Async Function PurchaseOrders(exs As List(Of Exception)) As Task(Of List(Of DTOPurchaseOrder))
        Dim iYea As Integer = ComboBoxYea.Text
        Dim itm As Integer
        Dim retval As New List(Of DTOPurchaseOrder)
        For Each itm In NumList()
            Dim oOrder = Await FEB2.PurchaseOrder.FromNum(Current.Session.Emp, iYea, itm, exs)
            If oOrder Is Nothing Then
                exs.Add(New Exception("comanda " & iYea.ToString & "." & itm.ToString & " no registrada"))
            Else
                retval.Add(oOrder)
            End If
        Next
        Return retval
    End Function


    Private Async Function Deliveries(exs As List(Of Exception)) As Task(Of List(Of DTODelivery))
        Dim iYea As Integer = ComboBoxYea.Text

        Dim retval As New List(Of DTODelivery)
        For Each itm As String In NumList()
            If itm > "" Then
                Dim IntGuion As Integer = itm.IndexOf("-")
                Select Case IntGuion
                    Case Is > 0
                        Dim FirstItm As Integer = itm.Substring(0, IntGuion)
                        Dim LastItm As Integer = itm.Substring(IntGuion + 1)
                        For tmp = FirstItm To LastItm
                            Dim oDelivery = Await FEB2.Delivery.FromNum(Current.Session.Emp, iYea, tmp, exs)
                            If oDelivery Is Nothing Then
                                exs.Add(New Exception("albará " & iYea.ToString & "." & tmp.ToString & " no registrat"))
                            Else
                                retval.Add(oDelivery)
                            End If
                        Next
                    Case Else
                        Dim oDelivery = Await FEB2.Delivery.FromNum(Current.Session.Emp, iYea, itm, exs)
                        If oDelivery Is Nothing Then
                            exs.Add(New Exception("albará " & iYea.ToString & "." & itm.ToString & " no registrat"))
                        Else

                            retval.Add(oDelivery)
                        End If
                End Select
            End If
        Next
        Return retval
    End Function

    Private Async Function Invoices(exs As List(Of Exception)) As Task(Of List(Of DTOInvoice))
        Dim iYea As Integer = ComboBoxYea.Text
        Dim itm As String
        Dim tmp As Long
        Dim FirstItm As Long
        Dim LastItm As Long
        Dim retval As New List(Of DTOInvoice)
        For Each itm In NumList()
            Dim IntGuion As Integer = itm.IndexOf("-")
            Select Case IntGuion
                Case Is > 0
                    FirstItm = itm.Substring(0, IntGuion)
                    LastItm = itm.Substring(IntGuion + 1)
                    For tmp = FirstItm To LastItm
                        Dim oInvoice = Await FEB2.Invoice.FromNum(exs, Current.Session.Emp, iYea, tmp)
                        If exs.Count = 0 Then
                            If oInvoice Is Nothing Then
                                exs.Add(New Exception("factura " & iYea.ToString & "." & tmp.ToString & " no registrada"))
                            Else
                                retval.Add(oInvoice)
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Next
                Case Else
                    tmp = itm
                    Dim oInvoice = Await FEB2.Invoice.FromNum(exs, Current.Session.Emp, iYea, tmp)
                    If exs.Count = 0 Then
                        If oInvoice Is Nothing Then
                            exs.Add(New Exception("factura " & iYea.ToString & "." & tmp.ToString & " no registrada"))
                        Else
                            retval.Add(oInvoice)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
        Next
        Return retval
    End Function

    Private Async Function Ccas(exs As List(Of Exception)) As Task(Of List(Of DTOCca))
        Dim iYea As Integer = ComboBoxYea.Text
        Dim itm As String
        Dim tmp As Long
        Dim FirstItm As Long
        Dim LastItm As Long
        Dim retval As New List(Of DTOCca)
        Dim oCca As DTOCca = Nothing
        For Each itm In NumList()
            Dim IntGuion As Integer = itm.IndexOf("-")
            Select Case IntGuion
                Case Is > 0
                    FirstItm = itm.Substring(0, IntGuion)
                    LastItm = itm.Substring(IntGuion + 1)
                    For tmp = FirstItm To LastItm
                        oCca = Await FEB2.Cca.FromNum(Current.Session.Emp, ComboBoxYea.Text, tmp, exs)
                        If oCca Is Nothing Then
                            exs.Add(New Exception("assentament " & iYea.ToString & "." & tmp.ToString & " no registrat"))
                        Else
                            retval.Add(oCca)
                        End If
                    Next
                Case Else
                    oCca = Await FEB2.Cca.FromNum(Current.Session.Emp, ComboBoxYea.Text, itm, exs)
                    If oCca Is Nothing Then
                        exs.Add(New Exception("assentament " & iYea.ToString & "." & itm.ToString & " no registrat"))
                    Else
                        retval.Add(oCca)
                    End If
            End Select
        Next

        Return retval
    End Function

    Private Sub LoadYeas()
        Dim i As Integer
        Dim IntYea As Integer = Year(Now)
        For i = IntYea To 1985 Step -1
            ComboBoxYea.Items.Add(i)
        Next
        ComboBoxYea.SelectedIndex = 0
    End Sub


    Private Function NumList() As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim source As String = TextBoxNum.Text
        Dim lines() As String = source.Split(Environment.NewLine)
        For Each sLine As String In lines
            If IsNumeric(sLine) Then
                retval.Add(CInt(sLine))
            End If
        Next
        Return retval
    End Function


    Private Async Sub ButtonFrx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonFrx.Click
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await Deliveries(exs)
        If exs.Count = 0 Then
            Dim oInvoice = Await FEB2.Invoice.Factory(exs, Current.Session.Emp, oDeliveries)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_Invoice(oInvoice)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub SetButtons()
        Select Case NumList.Count
            Case 0
                ButtonZoom.Enabled = False
                ButtonCopyLink.Enabled = False
                ButtonFrx.Enabled = False
            Case 1
                ButtonZoom.Enabled = True
                ButtonCopyLink.Enabled = True
                ButtonFrx.Enabled = (mStyle = Styles.Albara)
            Case Else
                ButtonZoom.Enabled = False
                ButtonCopyLink.Enabled = True
                ButtonFrx.Enabled = (mStyle = Styles.Albara)
        End Select
    End Sub

    Private Sub TextBoxNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNum.TextChanged
        SetButtons()
    End Sub

    Private Async Sub ButtonZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonZoom.Click
        Dim exs As New List(Of Exception)
        Select Case mStyle
            Case Styles.Comanda
                Dim oOrders As List(Of DTOPurchaseOrder) = Await PurchaseOrders(exs)
                If exs.Count = 0 Then
                    If oOrders.Count > 0 Then
                        root.ShowPurchaseOrder(oOrders.First)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Case Styles.Albara
                Dim oDeliveries = Await Deliveries(exs)
                If exs.Count = 0 Then
                    If oDeliveries.Count > 0 Then
                        Dim oDelivery As DTODelivery = oDeliveries.First
                        Select Case oDelivery.Cod
                            Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                                Dim oCustomer As DTOCustomer = oDelivery.Contact
                                If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                                    Dim oFrm As New Frm_AlbNew2(oDelivery)
                                    oFrm.Show()
                                Else
                                    UIHelper.WarnError(exs)
                                End If
                            Case DTOPurchaseOrder.Codis.Proveidor
                                Dim oProveidor As DTOProveidor = oDelivery.Contact
                                If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oProveidor, DTOAlbBloqueig.Codis.ALB, exs) Then
                                    Dim oFrm As New Frm_AlbNew2(oDelivery)
                                    oFrm.Show()
                                Else
                                    UIHelper.WarnError(exs)
                                End If
                            Case DTOPurchaseOrder.Codis.Traspas
                                Dim oFrm As New Frm_AlbTraspas(oDelivery)
                                With oFrm
                                    .Show()
                                End With
                        End Select
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Case Styles.Factura
                Dim oInvoices As List(Of DTOInvoice) = Await Invoices(exs)
                If oInvoices.Count > 0 Then
                    Dim oFrm As New Frm_Invoice(oInvoices.First)
                    oFrm.Show()
                End If
            Case Styles.Assentament
                Dim oCcas As List(Of DTOCca) = Await Ccas(exs)
                If exs.Count = 0 Then
                    Dim oFrm As New Frm_Cca(oCcas.First)
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            Case Styles.Incidencia
                Dim oIncidencias = Await Incidencias()
                If oIncidencias.Count > 0 Then
                    Dim oIncidencia = oIncidencias.First
                    If oIncidencia Is Nothing Then
                        UIHelper.WarnError(String.Format("no hi ha cap incidencia amb aquest numero a l'any {0}", ComboBoxYea.Text))
                    Else
                        Dim oFrm As New Frm_Incidencia(oIncidencias.First)
                        oFrm.Show()
                    End If
                End If
        End Select
    End Sub

    Private Sub ContextMenu1_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenu1.Popup
        With ContextMenu1.MenuItems
            .Clear()
            Select Case mStyle
                Case Styles.Factura


            End Select
        End With
    End Sub

    Private Async Sub ButtonCopyLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCopyLink.Click
        Dim exs As New List(Of Exception)
        Select Case mStyle
            Case Styles.Comanda
                'root.ShowPdc(Pdcs(0))
            Case Styles.Albara
                'root.ShowAlb(Albs(0))
            Case Styles.Factura
                Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
                For Each oInvoice As DTOInvoice In Await Invoices(exs)
                    FEB2.Invoice.Load(oInvoice, exs)
                    Dim sDisplayText As String = "factura " & DTOInvoice.FormattedId(oInvoice)
                    Dim sUrl = FEB2.Invoice.PdfUrl(oInvoice, True)
                    Dim oRow As MatHelperStd.ExcelHelper.Row = oSheet.AddRow()
                    oRow.AddCell(sDisplayText, sUrl)
                Next
                If exs.Count = 0 Then
                    If Not UIHelper.ShowExcel(oSheet, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Case Styles.Assentament
                Dim oCcas = Await Ccas(exs)
                If exs.Count = 0 Then
                    Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
                    For Each oCca As DTOCca In oCcas
                        If FEB2.Cca.Load(oCca, exs) Then
                            Dim sDisplayText As String = String.Format("assentament {0:yyyy}.{1:000000} {2:dd/MM/yy} {3}", oCca.Fch, oCca.Id, oCca.Fch, oCca.Concept)
                            Dim sUrl As String = FEB2.DocFile.DownloadUrl(oCca.DocFile, True)
                            Dim oRow As MatHelperStd.ExcelHelper.Row = oSheet.AddRow()
                            oRow.AddCell(sDisplayText, sUrl)
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Next
                    If Not UIHelper.ShowExcel(oSheet, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select
    End Sub



End Class

