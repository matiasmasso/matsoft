

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
    Friend WithEvents ButtonPrint As System.Windows.Forms.Button
    Friend WithEvents ButtonZoom As System.Windows.Forms.Button
    Friend WithEvents ButtonFrx As System.Windows.Forms.Button
    Friend WithEvents ButtonCopyLink As System.Windows.Forms.Button
    Friend WithEvents CheckBoxrenumerat As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Doc_Select))
        Me.ComboBoxYea = New System.Windows.Forms.ComboBox
        Me.TextBoxNum = New System.Windows.Forms.TextBox
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.Label2 = New System.Windows.Forms.Label
        Me.LabelNum = New System.Windows.Forms.Label
        Me.ButtonPrint = New System.Windows.Forms.Button
        Me.ButtonZoom = New System.Windows.Forms.Button
        Me.ButtonFrx = New System.Windows.Forms.Button
        Me.ButtonCopyLink = New System.Windows.Forms.Button
        Me.CheckBoxrenumerat = New System.Windows.Forms.CheckBox
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
        Me.TextBoxNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNum.ContextMenu = Me.ContextMenu1
        Me.TextBoxNum.Location = New System.Drawing.Point(16, 72)
        Me.TextBoxNum.Multiline = True
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(80, 170)
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
        'ButtonPrint
        '
        Me.ButtonPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrint.Enabled = False
        Me.ButtonPrint.Image = CType(resources.GetObject("ButtonPrint.Image"), System.Drawing.Image)
        Me.ButtonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonPrint.Location = New System.Drawing.Point(104, 202)
        Me.ButtonPrint.Name = "ButtonPrint"
        Me.ButtonPrint.Size = New System.Drawing.Size(104, 40)
        Me.ButtonPrint.TabIndex = 7
        Me.ButtonPrint.Text = "IMPRIMIR"
        Me.ButtonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ButtonZoom
        '
        Me.ButtonZoom.Enabled = False
        Me.ButtonZoom.Location = New System.Drawing.Point(104, 81)
        Me.ButtonZoom.Name = "ButtonZoom"
        Me.ButtonZoom.Size = New System.Drawing.Size(104, 40)
        Me.ButtonZoom.TabIndex = 4
        Me.ButtonZoom.Text = "ZOOM"
        '
        'ButtonFrx
        '
        Me.ButtonFrx.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFrx.Enabled = False
        Me.ButtonFrx.Image = CType(resources.GetObject("ButtonFrx.Image"), System.Drawing.Image)
        Me.ButtonFrx.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonFrx.Location = New System.Drawing.Point(104, 162)
        Me.ButtonFrx.Name = "ButtonFrx"
        Me.ButtonFrx.Size = New System.Drawing.Size(104, 40)
        Me.ButtonFrx.TabIndex = 6
        Me.ButtonFrx.Text = "FACTURAR"
        Me.ButtonFrx.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ButtonCopyLink
        '
        Me.ButtonCopyLink.Enabled = False
        Me.ButtonCopyLink.Image = My.Resources.Resources.Copy
        Me.ButtonCopyLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCopyLink.Location = New System.Drawing.Point(104, 121)
        Me.ButtonCopyLink.Name = "ButtonCopyLink"
        Me.ButtonCopyLink.Size = New System.Drawing.Size(104, 40)
        Me.ButtonCopyLink.TabIndex = 5
        Me.ButtonCopyLink.Text = "COPIAR LINK"
        Me.ButtonCopyLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CheckBoxrenumerat
        '
        Me.CheckBoxrenumerat.AutoSize = True
        Me.CheckBoxrenumerat.Location = New System.Drawing.Point(104, 56)
        Me.CheckBoxrenumerat.Name = "CheckBoxrenumerat"
        Me.CheckBoxrenumerat.Size = New System.Drawing.Size(73, 17)
        Me.CheckBoxrenumerat.TabIndex = 8
        Me.CheckBoxrenumerat.Text = "renumerat"
        Me.CheckBoxrenumerat.UseVisualStyleBackColor = True
        Me.CheckBoxrenumerat.Visible = False
        '
        'Frm_Doc_Select
        '
        Me.ClientSize = New System.Drawing.Size(216, 248)
        Me.Controls.Add(Me.CheckBoxrenumerat)
        Me.Controls.Add(Me.ButtonCopyLink)
        Me.Controls.Add(Me.ButtonFrx)
        Me.Controls.Add(Me.ButtonZoom)
        Me.Controls.Add(Me.ButtonPrint)
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
    Private mEmp as DTOEmp = BLL.BLLApp.Emp


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
                    LabelNum.Text = "Albará:"
                Case Styles.Factura
                    LabelNum.Text = "Factura:"
                Case Styles.Assentament
                    LabelNum.Text = "Assentament:"
                    CheckBoxrenumerat.Visible = True
                Case Styles.Incidencia
                    LabelNum.Text = "Incidencia:"
            End Select
        End Set
    End Property

    Private Function Incidencias() As List(Of DTOIncidencia)
        Dim exs as New List(Of exception)
        Dim itm As String
        Dim retval As New List(Of DTOIncidencia)
        For Each itm In NumList()
            Dim oIncidencia As DTOIncidencia = BLL_Incidencia.FromNum(itm)
            retval.Add(oIncidencia)
        Next
        Return retval
    End Function

    Private Function Pdcs() As MaxiSrvr.Pdcs
        Dim exs as New List(Of exception)
        Dim iYea As Integer = ComboBoxYea.Text
        Dim itm As Integer
        Dim oPdcs As New MaxiSrvr.Pdcs
        For Each itm In NumList()
            Dim oPdc As Pdc = Pdc.FromNum(mEmp, iYea, itm)
            If oPdc Is Nothing Then
                exs.Add(New Exception("comanda " & iYea.ToString & "." & itm.ToString & " no registrada"))
            Else
                oPdcs.Add(oPdc)
            End If
        Next

        If exs.Count > 0 Then
            MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
        Return oPdcs
    End Function

    Private Function Albs() As MaxiSrvr.Albs
        Dim exs as New List(Of exception)
        Dim iYea As Integer = ComboBoxYea.Text
        Dim itm As String
        Dim tmp As Long
        Dim FirstItm As Long
        Dim LastItm As Long
        Dim oItms As New MaxiSrvr.Albs
        For Each itm In NumList()
            If itm > "" Then
                Dim IntGuion As Integer = itm.IndexOf("-")
                Select Case IntGuion
                    Case Is > 0
                        FirstItm = itm.Substring(0, IntGuion)
                        LastItm = itm.Substring(IntGuion + 1)
                        For tmp = FirstItm To LastItm
                            Dim oAlb As Alb = MaxiSrvr.Alb.FromNum(mEmp, iYea, tmp)
                            If oAlb Is Nothing Then
                                exs.Add(New Exception("albará " & iYea.ToString & "." & tmp.ToString & " no registrat"))
                            Else
                                oItms.Add(oAlb)
                            End If
                        Next
                    Case Else
                        Dim oAlb As Alb = MaxiSrvr.Alb.FromNum(mEmp, iYea, itm)
                        If oAlb Is Nothing Then
                            exs.Add(New Exception("albará " & iYea.ToString & "." & itm.ToString & " no registrat"))
                        Else
                            oItms.Add(oAlb)
                        End If
                End Select
            End If
        Next
        Return oItms
    End Function

    Private Function Fras() As MaxiSrvr.Fras
        Dim exs as New List(Of exception)
        Dim iYea As Integer = ComboBoxYea.Text
        Dim itm As String
        Dim tmp As Long
        Dim FirstItm As Long
        Dim LastItm As Long
        Dim oFras As New MaxiSrvr.Fras
        For Each itm In NumList()
            Dim IntGuion As Integer = itm.IndexOf("-")
            Select Case IntGuion
                Case Is > 0
                    FirstItm = itm.Substring(0, IntGuion)
                    LastItm = itm.Substring(IntGuion + 1)
                    For tmp = FirstItm To LastItm
                        Dim oFra As Fra = MaxiSrvr.Fra.FromNum(mEmp, iYea, tmp)
                        If oFra Is Nothing Then
                            exs.Add(New Exception("factura " & iYea.ToString & "." & tmp.ToString & " no registrada"))
                        Else
                            oFras.Add(oFra)
                        End If
                    Next
                Case Else
                    Dim oFra As Fra = MaxiSrvr.Fra.FromNum(mEmp, iYea, itm)
                    If oFra Is Nothing Then
                        exs.Add(New Exception("factura " & iYea.ToString & "." & tmp.ToString & " no registrada"))
                    Else
                        oFras.Add(oFra)
                    End If
            End Select
        Next
        Return oFras
    End Function

    Private Function Ccas() As MaxiSrvr.Ccas
        Dim exs as New List(Of exception)
        Dim iYea As Integer = ComboBoxYea.Text
        Dim itm As String
        Dim tmp As Long
        Dim FirstItm As Long
        Dim LastItm As Long
        Dim oCcas As New MaxiSrvr.Ccas
        Dim oCca As Cca = Nothing
        For Each itm In NumList()
            Dim IntGuion As Integer = itm.IndexOf("-")
            Select Case IntGuion
                Case Is > 0
                    FirstItm = itm.Substring(0, IntGuion)
                    LastItm = itm.Substring(IntGuion + 1)
                    For tmp = FirstItm To LastItm
                        If CheckBoxrenumerat.Checked Then
                            oCca = Cca.FromRenumeratedIndex(mEmp, ComboBoxYea.Text, tmp)
                        Else
                            oCca = MaxiSrvr.Cca.FromNum(mEmp, ComboBoxYea.Text, tmp)
                        End If
                        If oCca Is Nothing Then
                            exs.Add(New Exception("assentament " & iYea.ToString & "." & tmp.ToString & " no registrat"))
                        Else
                            oCcas.Add(oCca)
                        End If
                    Next
                Case Else
                    If CheckBoxrenumerat.Checked Then
                        oCca = Cca.FromRenumeratedIndex(mEmp, ComboBoxYea.Text, itm)
                    Else
                        oCca = MaxiSrvr.Cca.FromNum(mEmp, ComboBoxYea.Text, itm)
                    End If
                    If oCca Is Nothing Then
                        exs.Add(New Exception("assentament " & iYea.ToString & "." & itm.ToString & " no registrat"))
                    Else
                        oCcas.Add(oCca)
                    End If
            End Select
        Next
        If exs.Count > 0 Then
            MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

        Return oCcas
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

    Private Sub ButtonPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPrint.Click
        Dim exs as New List(Of exception)
        Select Case mStyle
            Case Styles.Comanda
                root.PrintPdcs(Pdcs)
            Case Styles.Albara
                root.PrintAlbs(Albs)
            Case Styles.Factura
                root.PrintFras(Fras)
            Case Styles.Assentament
        End Select
    End Sub

    Private Sub ButtonFrx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonFrx.Click
        root.ExeFacturacio(Albs)
    End Sub

    Private Sub SetButtons()
        Select Case NumList.Count
            Case 0
                ButtonZoom.Enabled = False
                ButtonCopyLink.Enabled = False
                ButtonFrx.Enabled = False
                ButtonPrint.Enabled = False
            Case 1
                ButtonZoom.Enabled = True
                ButtonCopyLink.Enabled = True
                ButtonFrx.Enabled = (mStyle = Styles.Albara)
                ButtonPrint.Enabled = True
            Case Else
                ButtonZoom.Enabled = False
                ButtonCopyLink.Enabled = True
                ButtonFrx.Enabled = (mStyle = Styles.Albara)
                ButtonPrint.Enabled = True
        End Select
    End Sub

    Private Sub TextBoxNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNum.TextChanged
        SetButtons()
    End Sub

    Private Sub ButtonZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonZoom.Click
        Select Case mStyle
            Case Styles.Comanda
                Dim oPdcs As Pdcs = Pdcs()
                If oPdcs.Count > 0 Then
                    Dim oPurchaseOrder As New DTOPurchaseOrder(oPdcs(0).Guid)
                    root.ShowPurchaseOrder(oPurchaseOrder)
                End If
            Case Styles.Albara
                Dim oAlbs As Albs = Albs()
                If oAlbs.Count > 0 Then
                    root.ShowAlb(Albs(0))
                End If
            Case Styles.Factura
                Dim oFras As Fras = Fras()
                If oFras.Count > 0 Then
                    root.ShowFra(Fras(0))
                End If
            Case Styles.Assentament
                Dim oCcas As Ccas = Ccas()
                If oCcas.Count > 0 Then
                    root.ShowCca(Ccas(0))
                End If
            Case Styles.Incidencia
                Dim oIncidencias As List(Of DTOIncidencia) = Incidencias()
                If oIncidencias.Count > 0 Then
                    Dim oFrm As New Frm_Incidencia(Incidencias(0))
                    oFrm.Show()
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

    Private Sub ButtonCopyLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCopyLink.Click
        Select Case mStyle
            Case Styles.Comanda
                'root.ShowPdc(Pdcs(0))
            Case Styles.Albara
                'root.ShowAlb(Albs(0))
            Case Styles.Factura
                Dim oArrayText As New ArrayList
                Dim oArrayDocFile As New ArrayList
                Dim oFra As Fra = Nothing
                Dim sText As String = ""
                Dim sGuid As String = ""
                For Each oFra In Fras()
                    sText = "factura " & oFra.Formatted
                    oArrayText.Add(sText)
                    oArrayDocFile.Add(oFra.Cca.DocFile)
                Next
                MatExcel.CopyLinksToClipboard(oArrayText, oArrayDocFile).Visible = True
            Case Styles.Assentament
                Dim oArrayText As New ArrayList
                Dim oArrayDocFile As New ArrayList
                Dim oCca As Cca = Nothing
                Dim sText As String = ""
                Dim sGuid As String = ""
                For Each oCca In GetCcasFromAux()
                    sText = "assentament " & oCca.yea.ToString & "." & Format(oCca.AuxCca, "000000") & " " & oCca.fch.ToShortDateString & " " & oCca.Txt
                    oArrayText.Add(sText)
                    oArrayDocFile.Add(oCca.DocFile)
                Next
                MatExcel.CopyLinksToClipboard(oArrayText, oArrayDocFile).Visible = True
        End Select
    End Sub

    Private Function GetCcasFromAux() As Ccas
        Dim itm As String
        Dim tmp As Long
        Dim FirstItm As Long
        Dim LastItm As Long
        Dim oCcas As New MaxiSrvr.Ccas
        Dim oCca As Cca

        For Each itm In NumList()
            Dim IntGuion As Integer = itm.IndexOf("-")
            Select Case IntGuion
                Case Is > 0
                    FirstItm = itm.Substring(0, IntGuion)
                    LastItm = itm.Substring(IntGuion + 1)
                    For tmp = FirstItm To LastItm
                        oCca = Cca.FromRenumeratedIndex(mEmp, ComboBoxYea.Text, tmp)
                        If oCca IsNot Nothing Then
                            oCcas.Add(oCca)
                        End If
                    Next
                Case Else
                    If IsNumeric(itm) Then
                        oCcas.Add(Cca.FromRenumeratedIndex(mEmp, ComboBoxYea.Text, itm))
                    End If
            End Select
        Next
        Return oCcas
    End Function


End Class

