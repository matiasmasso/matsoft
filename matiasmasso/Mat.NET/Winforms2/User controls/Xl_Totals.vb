Public Class Xl_Totals
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxDto As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxDpp As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_PercentReq As Xl_Percent
    Friend WithEvents Xl_PercentIVA As Xl_Percent
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxReq As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PanelCash As System.Windows.Forms.Panel
    Friend WithEvents CheckBoxSuplto As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_AmtSuplto As Xl_Amount
    Friend WithEvents Xl_AmtTot As Xl_Amount
    Friend WithEvents Xl_AmtLiq As Xl_Amount
    Friend WithEvents Xl_AmtSuma As Xl_Amount
    Friend WithEvents Xl_PercentDto As Xl_Percent
    Friend WithEvents Xl_PercentDpp As Xl_Percent
    Friend WithEvents Xl_AmtBase As Xl_Amount
    Friend WithEvents LabelLiquid As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Xl_AmtSuma = New Xl_Amount
        Me.Label1 = New System.Windows.Forms.Label
        Me.CheckBoxDto = New System.Windows.Forms.CheckBox
        Me.CheckBoxDpp = New System.Windows.Forms.CheckBox
        Me.Xl_PercentDto = New Xl_Percent
        Me.Xl_PercentDpp = New Xl_Percent
        Me.Label2 = New System.Windows.Forms.Label
        Me.Xl_AmtBase = New Xl_Amount
        Me.Xl_PercentReq = New Xl_Percent
        Me.Xl_PercentIVA = New Xl_Percent
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox
        Me.CheckBoxReq = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Xl_AmtTot = New Xl_Amount
        Me.PanelCash = New System.Windows.Forms.Panel
        Me.LabelLiquid = New System.Windows.Forms.Label
        Me.Xl_AmtLiq = New Xl_Amount
        Me.CheckBoxSuplto = New System.Windows.Forms.CheckBox
        Me.Xl_AmtSuplto = New Xl_Amount
        Me.PanelCash.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_AmtSuma
        '
        Me.Xl_AmtSuma.Amt = Nothing
        Me.Xl_AmtSuma.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.Xl_AmtSuma.Location = New System.Drawing.Point(0, 16)
        Me.Xl_AmtSuma.Name = "Xl_AmtSuma"
        Me.Xl_AmtSuma.Size = New System.Drawing.Size(80, 20)
        Me.Xl_AmtSuma.TabIndex = 0
        Me.Xl_AmtSuma.TabStop = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Suma"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CheckBoxDto
        '
        Me.CheckBoxDto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxDto.Location = New System.Drawing.Point(80, 0)
        Me.CheckBoxDto.Name = "CheckBoxDto"
        Me.CheckBoxDto.Size = New System.Drawing.Size(40, 16)
        Me.CheckBoxDto.TabIndex = 2
        Me.CheckBoxDto.Text = "dte"
        Me.CheckBoxDto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CheckBoxDpp
        '
        Me.CheckBoxDpp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxDpp.Location = New System.Drawing.Point(112, 0)
        Me.CheckBoxDpp.Name = "CheckBoxDpp"
        Me.CheckBoxDpp.Size = New System.Drawing.Size(48, 16)
        Me.CheckBoxDpp.TabIndex = 3
        Me.CheckBoxDpp.Text = "dpp"
        Me.CheckBoxDpp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Xl_PercentDto
        '
        Me.Xl_PercentDto.Location = New System.Drawing.Point(80, 16)
        Me.Xl_PercentDto.Name = "Xl_PercentDto"
        Me.Xl_PercentDto.Size = New System.Drawing.Size(40, 20)
        Me.Xl_PercentDto.TabIndex = 4
        Me.Xl_PercentDto.Value = 0.0!
        Me.Xl_PercentDto.Visible = False
        '
        'Xl_PercentDpp
        '
        Me.Xl_PercentDpp.Location = New System.Drawing.Point(120, 16)
        Me.Xl_PercentDpp.Name = "Xl_PercentDpp"
        Me.Xl_PercentDpp.Size = New System.Drawing.Size(40, 20)
        Me.Xl_PercentDpp.TabIndex = 5
        Me.Xl_PercentDpp.Value = 0.0!
        Me.Xl_PercentDpp.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(156, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Base Imponible"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Xl_AmtBase
        '
        Me.Xl_AmtBase.Amt = Nothing
        Me.Xl_AmtBase.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.Xl_AmtBase.Location = New System.Drawing.Point(160, 16)
        Me.Xl_AmtBase.Name = "Xl_AmtBase"
        Me.Xl_AmtBase.Size = New System.Drawing.Size(80, 20)
        Me.Xl_AmtBase.TabIndex = 6
        Me.Xl_AmtBase.TabStop = False
        '
        'Xl_PercentReq
        '
        Me.Xl_PercentReq.Location = New System.Drawing.Point(280, 16)
        Me.Xl_PercentReq.Name = "Xl_PercentReq"
        Me.Xl_PercentReq.Size = New System.Drawing.Size(40, 20)
        Me.Xl_PercentReq.TabIndex = 11
        Me.Xl_PercentReq.Value = 0.0!
        Me.Xl_PercentReq.Visible = False
        '
        'Xl_PercentIVA
        '
        Me.Xl_PercentIVA.Location = New System.Drawing.Point(240, 16)
        Me.Xl_PercentIVA.Name = "Xl_PercentIVA"
        Me.Xl_PercentIVA.Size = New System.Drawing.Size(40, 20)
        Me.Xl_PercentIVA.TabIndex = 10
        Me.Xl_PercentIVA.Value = 0.0!
        Me.Xl_PercentIVA.Visible = False
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIVA.Location = New System.Drawing.Point(232, 0)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(48, 16)
        Me.CheckBoxIVA.TabIndex = 8
        Me.CheckBoxIVA.Text = "IVA"
        Me.CheckBoxIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CheckBoxReq
        '
        Me.CheckBoxReq.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxReq.Location = New System.Drawing.Point(272, 0)
        Me.CheckBoxReq.Name = "CheckBoxReq"
        Me.CheckBoxReq.Size = New System.Drawing.Size(48, 16)
        Me.CheckBoxReq.TabIndex = 9
        Me.CheckBoxReq.Text = "Req"
        Me.CheckBoxReq.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxReq.Visible = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(320, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Total"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Xl_AmtTot
        '
        Me.Xl_AmtTot.Amt = Nothing
        Me.Xl_AmtTot.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.Xl_AmtTot.Location = New System.Drawing.Point(320, 16)
        Me.Xl_AmtTot.Name = "Xl_AmtTot"
        Me.Xl_AmtTot.Size = New System.Drawing.Size(80, 20)
        Me.Xl_AmtTot.TabIndex = 12
        Me.Xl_AmtTot.TabStop = False
        '
        'PanelCash
        '
        Me.PanelCash.Controls.Add(Me.LabelLiquid)
        Me.PanelCash.Controls.Add(Me.Xl_AmtLiq)
        Me.PanelCash.Controls.Add(Me.CheckBoxSuplto)
        Me.PanelCash.Controls.Add(Me.Xl_AmtSuplto)
        Me.PanelCash.Location = New System.Drawing.Point(400, 0)
        Me.PanelCash.Name = "PanelCash"
        Me.PanelCash.Size = New System.Drawing.Size(168, 40)
        Me.PanelCash.TabIndex = 18
        '
        'LabelLiquid
        '
        Me.LabelLiquid.Location = New System.Drawing.Point(88, 0)
        Me.LabelLiquid.Name = "LabelLiquid"
        Me.LabelLiquid.Size = New System.Drawing.Size(80, 16)
        Me.LabelLiquid.TabIndex = 21
        Me.LabelLiquid.Text = "Liquid"
        Me.LabelLiquid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LabelLiquid.Visible = False
        '
        'Xl_AmtLiq
        '
        Me.Xl_AmtLiq.Amt = Nothing
        Me.Xl_AmtLiq.BackColor = System.Drawing.Color.LightBlue
        Me.Xl_AmtLiq.Location = New System.Drawing.Point(88, 16)
        Me.Xl_AmtLiq.Name = "Xl_AmtLiq"
        Me.Xl_AmtLiq.Size = New System.Drawing.Size(80, 20)
        Me.Xl_AmtLiq.TabIndex = 20
        Me.Xl_AmtLiq.TabStop = False
        Me.Xl_AmtLiq.Visible = False
        '
        'CheckBoxSuplto
        '
        Me.CheckBoxSuplto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxSuplto.Location = New System.Drawing.Point(0, 0)
        Me.CheckBoxSuplto.Name = "CheckBoxSuplto"
        Me.CheckBoxSuplto.Size = New System.Drawing.Size(88, 16)
        Me.CheckBoxSuplto.TabIndex = 19
        Me.CheckBoxSuplto.Text = "Suplement"
        Me.CheckBoxSuplto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxSuplto.Visible = False
        '
        'Xl_AmtSuplto
        '
        Me.Xl_AmtSuplto.Amt = Nothing
        Me.Xl_AmtSuplto.BackColor = System.Drawing.Color.LightBlue
        Me.Xl_AmtSuplto.Location = New System.Drawing.Point(8, 16)
        Me.Xl_AmtSuplto.Name = "Xl_AmtSuplto"
        Me.Xl_AmtSuplto.Size = New System.Drawing.Size(80, 20)
        Me.Xl_AmtSuplto.TabIndex = 18
        Me.Xl_AmtSuplto.Visible = False
        '
        'Xl_Totals
        '
        Me.Controls.Add(Me.PanelCash)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_AmtTot)
        Me.Controls.Add(Me.Xl_PercentReq)
        Me.Controls.Add(Me.Xl_PercentIVA)
        Me.Controls.Add(Me.Xl_AmtBase)
        Me.Controls.Add(Me.Xl_PercentDpp)
        Me.Controls.Add(Me.Xl_PercentDto)
        Me.Controls.Add(Me.CheckBoxDto)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_AmtSuma)
        Me.Controls.Add(Me.CheckBoxDpp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBoxIVA)
        Me.Controls.Add(Me.CheckBoxReq)
        Me.Name = "Xl_Totals"
        Me.Size = New System.Drawing.Size(568, 40)
        Me.PanelCash.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mCash As DTOCustomer.CashCodes
    Private mCredit As DTOAmt
    Private mOutOfCredit As Boolean
    Private COLOR_CASH As System.Drawing.Color = System.Drawing.Color.LightBlue

    Public Event AfterUpdate()

    Public WriteOnly Property Credit() As DTOAmt
        Set(ByVal Value As DTOAmt)
            mCredit = Value
        End Set
    End Property

    Public Property Suma() As DTOAmt
        Get
            Return Xl_AmtSuma.Amt
        End Get
        Set(ByVal Value As DTOAmt)
            Xl_AmtSuma.Amt = Value
            Calcula()
        End Set
    End Property

    Public Property Dto() As Decimal
        Get
            Dim retval As Decimal
            If CheckBoxDto.Checked Then
                retval = Xl_PercentDto.Value
            End If
            Return retval
        End Get
        Set(ByVal Value As Decimal)
            If Value <> 0 Then
                CheckBoxDto.Checked = True
                Xl_PercentDto.Visible = True
            Else
                CheckBoxDto.Checked = False
                Xl_PercentDto.Visible = False
            End If
            Xl_PercentDto.Value = Value
            Calcula()
        End Set
    End Property

    Public Property Dpp() As Decimal
        Get
            Dim retval As Decimal
            If CheckBoxDpp.Checked Then
                retval = Xl_PercentDpp.Value
            End If
            Return retval
        End Get
        Set(ByVal Value As Decimal)
            If Value <> 0 Then
                CheckBoxDpp.Checked = True
                Xl_PercentDpp.Visible = True
            Else
                CheckBoxDpp.Checked = False
                Xl_PercentDpp.Visible = False
            End If
            Xl_PercentDpp.Value = Value
            Calcula()
        End Set
    End Property

    Public ReadOnly Property BaseImponible() As DTOAmt
        Get
            Return Xl_AmtBase.Amt
        End Get
    End Property

    Public Property IVA() As Decimal
        Get
            Dim retval As Decimal = 0
            If CheckBoxIVA.Checked Then
                retval = Xl_PercentIVA.Value
            End If
            Return retval
        End Get
        Set(ByVal Value As Decimal)
            If Value <> 0 Then
                CheckBoxIVA.Checked = True
                Xl_PercentIVA.Visible = True
            Else
                CheckBoxIVA.Checked = False
                Xl_PercentIVA.Visible = False
            End If
            Xl_PercentIVA.Value = Value
            Calcula()
        End Set
    End Property

    Public Property Req() As Decimal
        Get
            Dim retval As Decimal
            If CheckBoxReq.Checked Then
                retval = Xl_PercentReq.Value
            End If
            Return retval
        End Get
        Set(ByVal Value As Decimal)
            If Value <> 0 Then
                CheckBoxReq.Checked = True
                Xl_PercentReq.Visible = True
            Else
                CheckBoxReq.Checked = False
                Xl_PercentReq.Visible = False
            End If
            Xl_PercentReq.Value = Value
            Calcula()
        End Set
    End Property

    Public ReadOnly Property Total() As DTOAmt
        Get
            Return Xl_AmtTot.Amt
        End Get
    End Property

    Public Property Suplemento() As DTOAmt

        Get
            If CheckBoxSuplto.Checked Then
                If Xl_AmtSuplto.Amt Is Nothing Then
                    Dim oCur As DTOCur = Xl_AmtSuma.Amt.Cur
                    Return DTOAmt.Factory(oCur)
                Else
                    Return Xl_AmtSuplto.Amt
                End If
            Else
                If Xl_AmtSuma.Amt IsNot Nothing Then
                    Dim oCur As DTOCur = Xl_AmtSuma.Amt.Cur
                    Return DTOAmt.Factory(oCur)
                Else
                    Return Nothing
                End If
            End If
        End Get
        Set(ByVal Value As DTOAmt)
            If Value Is Nothing Then
                CheckBoxSuplto.Checked = False
            Else
                Xl_AmtSuplto.Amt = Value
                CheckBoxSuplto.Checked = Not (Value.Val = 0)
                Calcula()
            End If
            Xl_AmtSuplto.Visible = CheckBoxSuplto.Checked
        End Set
    End Property

    Public Function Liquid() As DTOAmt
        If Xl_AmtLiq.Visible Then
            Return Xl_AmtLiq.Amt
        Else
            Return Total
        End If
    End Function

    Public WriteOnly Property Cash() As DTOCustomer.CashCodes
        Set(ByVal Value As DTOCustomer.CashCodes)
            mCash = Value
            Select Case mCash
                Case DTOCustomer.CashCodes.credit
                    CheckBoxSuplto.Visible = False
                    Xl_AmtSuplto.Visible = False
                    Xl_AmtLiq.Visible = False
                    LabelLiquid.Visible = False
                    If Xl_PercentDpp.Value <> 0 Then MsgBox("pronto pago sense reembols?", MsgBoxStyle.Exclamation, "MAT.NET")
                Case DTOCustomer.CashCodes.Reembols
                    CheckBoxSuplto.Visible = True
                    Xl_AmtLiq.BackColor = Xl_AmtSuplto.BackColor()
                Case DTOCustomer.CashCodes.TransferenciaPrevia
                    CheckBoxSuplto.Visible = True
                    Xl_AmtSuplto.Visible = False
                    Xl_AmtLiq.Visible = False
            End Select
            Calcula()
        End Set
    End Property

    Public ReadOnly Property OutOfCredit() As Boolean
        Get
            Return mOutOfCredit
        End Get
    End Property

    Private Sub Calcula()
        If Xl_AmtSuma.Amt Is Nothing Then Return

        Dim oTot As DTOAmt = Xl_AmtSuma.Amt.Clone

        Dim SngDto As Decimal
        If CheckBoxDto.Checked Then SngDto = Xl_PercentDto.Value
        Dim oDto As DTOAmt = oTot.Percent(SngDto)
        oTot.Substract(oDto)

        Dim SngDpp As Decimal
        If CheckBoxDpp.Checked Then SngDpp = Xl_PercentDpp.Value
        Dim oDpp As DTOAmt = oTot.Percent(SngDpp)
        oTot.Substract(oDpp)

        Dim oBas As DTOAmt = oTot.Clone
        Xl_AmtBase.Amt = oBas

        Dim SngIVA As Decimal
        If CheckBoxIVA.Checked Then SngIVA = Xl_PercentIVA.Value
        Dim oIVA As DTOAmt = oBas.Percent(SngIVA)
        oTot.Add(oIVA)

        Dim SngReq As Decimal
        If CheckBoxReq.Checked Then SngReq = Xl_PercentReq.Value
        Dim oReq As DTOAmt = oBas.Percent(SngReq)
        oTot.Add(oReq)

        Xl_AmtTot.Amt = oTot

        mOutOfCredit = False
        Select Case mCash
            Case DTOCustomer.CashCodes.credit
                If mCredit IsNot Nothing And Xl_AmtTot.Amt.Eur > 0 Then
                    mOutOfCredit = (Xl_AmtTot.Amt.Eur > mCredit.Eur)
                End If
            Case Else
                If CheckBoxSuplto.Checked Then
                    Dim oSuplto As DTOAmt = Xl_AmtSuplto.Amt
                    If oSuplto Is Nothing Then
                        oSuplto = DTOAmt.Factory(oTot.Cur)
                        Xl_AmtSuplto.Amt = oSuplto
                    End If
                    oTot.Add(oSuplto)
                End If
                Xl_AmtLiq.Amt = oTot
        End Select
        SetTotalColor()
    End Sub

    Private Sub SetTotalColor()
        Dim oColor As System.Drawing.Color
        Select Case mCash
            Case DTOCustomer.CashCodes.credit
                Select Case mOutOfCredit
                    Case True
                        oColor = Color.LightSalmon
                    Case False
                        oColor = System.Drawing.Color.White
                End Select
            Case DTOCustomer.CashCodes.Reembols
                oColor = System.Drawing.Color.LightBlue
            Case DTOCustomer.CashCodes.TransferenciaPrevia, DTOCustomer.CashCodes.Visa
                oColor = System.Drawing.Color.White
        End Select
        Xl_AmtTot.BackColor = oColor
    End Sub

    Private Sub CheckBoxDto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDto.CheckedChanged
        Xl_PercentDto.Visible = CheckBoxDto.Checked
        Calcula()
        SetDirty()
    End Sub

    Private Sub CheckBoxDpp_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDpp.CheckedChanged
        Xl_PercentDpp.Visible = CheckBoxDpp.Checked
        Calcula()
        SetDirty()
    End Sub

    Private Sub CheckBoxIVA_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxIVA.CheckedChanged
        Xl_PercentIVA.Visible = CheckBoxIVA.Checked
        If CheckBoxIVA.Checked Then
            Xl_PercentIVA.Value = DTOTax.Closest(DTOTax.Codis.Iva_Standard).Tipus
            CheckBoxReq.Visible = True
        Else
            CheckBoxReq.Visible = False
            CheckBoxReq.Checked = False
        End If
        Xl_PercentReq.Visible = CheckBoxReq.Checked
        Calcula()
        SetDirty()
    End Sub

    Private Sub CheckBoxReq_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxReq.CheckedChanged
        Xl_PercentReq.Visible = CheckBoxReq.Checked
        If CheckBoxReq.Checked Then
            Xl_PercentReq.Value = DTOTax.Closest(DTOTax.Codis.Recarrec_Equivalencia_Standard).Tipus
        End If
        Calcula()
        SetDirty()
    End Sub

    Private Sub CheckBoxSuplto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxSuplto.CheckedChanged
        Xl_AmtSuplto.Visible = CheckBoxSuplto.Checked
        Xl_AmtLiq.Visible = CheckBoxSuplto.Checked
        Calcula()
        SetDirty()
    End Sub

    Private Sub Xl_PercentDto_AfterUpdate(ByVal SngPercent As Object, ByVal e As MatEventArgs) Handles Xl_PercentDto.AfterUpdate
        Calcula()
        SetDirty()
    End Sub

    Private Sub Xl_PercentDpp_AfterUpdate(ByVal SngPercent As Object, ByVal e As MatEventArgs) Handles Xl_PercentDpp.AfterUpdate
        Calcula()
        SetDirty()
    End Sub

    Private Sub Xl_PercentIVA_AfterUpdate(ByVal SngPercent As Object, ByVal e As MatEventArgs) Handles Xl_PercentIVA.AfterUpdate
        Calcula()
        SetDirty()
    End Sub

    Private Sub Xl_PercentReq_AfterUpdate(ByVal SngPercent As Object, ByVal e As MatEventArgs) Handles Xl_PercentReq.AfterUpdate
        Calcula()
        SetDirty()
    End Sub

    Private Sub Xl_AmtSuplto_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtSuplto.AfterUpdate
        Calcula()
        SetDirty()
    End Sub

    Private Sub SetDirty()
        RaiseEvent AfterUpdate()
    End Sub
End Class
