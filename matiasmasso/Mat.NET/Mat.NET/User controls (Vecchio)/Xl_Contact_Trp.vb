

Public Class Xl_Contact_Trp
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxActivated As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxAllowReembolsos As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCubicatje As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFactor As System.Windows.Forms.TextBox
    Friend WithEvents ButtonTarifas As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNoCubicarPerSotaDe As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxTransportaMobiliari As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBoxNoCubicarPerSotaDe = New System.Windows.Forms.TextBox
        Me.ButtonTarifas = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxFactor = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxCubicatje = New System.Windows.Forms.TextBox
        Me.CheckBoxAllowReembolsos = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.CheckBoxActivated = New System.Windows.Forms.CheckBox
        Me.CheckBoxTransportaMobiliari = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxTransportaMobiliari)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TextBoxNoCubicarPerSotaDe)
        Me.GroupBox1.Controls.Add(Me.ButtonTarifas)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBoxFactor)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBoxCubicatje)
        Me.GroupBox1.Controls.Add(Me.CheckBoxAllowReembolsos)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBoxNom)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 256)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(156, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(135, 16)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "no cubicar per sota de:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNoCubicarPerSotaDe
        '
        Me.TextBoxNoCubicarPerSotaDe.Location = New System.Drawing.Point(298, 59)
        Me.TextBoxNoCubicarPerSotaDe.Name = "TextBoxNoCubicarPerSotaDe"
        Me.TextBoxNoCubicarPerSotaDe.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxNoCubicarPerSotaDe.TabIndex = 9
        '
        'ButtonTarifas
        '
        Me.ButtonTarifas.Location = New System.Drawing.Point(267, 164)
        Me.ButtonTarifas.Name = "ButtonTarifas"
        Me.ButtonTarifas.Size = New System.Drawing.Size(87, 24)
        Me.ButtonTarifas.TabIndex = 8
        Me.ButtonTarifas.Text = "TARIFAS"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(159, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(181, 40)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "seleccionar quan sigui més barato del X%"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(32, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "factor:"
        '
        'TextBoxFactor
        '
        Me.TextBoxFactor.Location = New System.Drawing.Point(96, 88)
        Me.TextBoxFactor.Name = "TextBoxFactor"
        Me.TextBoxFactor.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxFactor.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(32, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "cubicatje:"
        '
        'TextBoxCubicatje
        '
        Me.TextBoxCubicatje.Location = New System.Drawing.Point(96, 56)
        Me.TextBoxCubicatje.Name = "TextBoxCubicatje"
        Me.TextBoxCubicatje.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxCubicatje.TabIndex = 3
        '
        'CheckBoxAllowReembolsos
        '
        Me.CheckBoxAllowReembolsos.Location = New System.Drawing.Point(96, 128)
        Me.CheckBoxAllowReembolsos.Name = "CheckBoxAllowReembolsos"
        Me.CheckBoxAllowReembolsos.Size = New System.Drawing.Size(144, 16)
        Me.CheckBoxAllowReembolsos.TabIndex = 2
        Me.CheckBoxAllowReembolsos.Text = "Admet reembolsaments"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(32, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "nom:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(96, 32)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(258, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'CheckBoxActivated
        '
        Me.CheckBoxActivated.Location = New System.Drawing.Point(0, 0)
        Me.CheckBoxActivated.Name = "CheckBoxActivated"
        Me.CheckBoxActivated.Size = New System.Drawing.Size(72, 24)
        Me.CheckBoxActivated.TabIndex = 1
        Me.CheckBoxActivated.Text = "Activat"
        '
        'CheckBoxTransportaMobiliari
        '
        Me.CheckBoxTransportaMobiliari.Location = New System.Drawing.Point(96, 150)
        Me.CheckBoxTransportaMobiliari.Name = "CheckBoxTransportaMobiliari"
        Me.CheckBoxTransportaMobiliari.Size = New System.Drawing.Size(144, 16)
        Me.CheckBoxTransportaMobiliari.TabIndex = 11
        Me.CheckBoxTransportaMobiliari.Text = "transporta mobiliari"
        '
        'Xl_Contact_Trp
        '
        Me.Controls.Add(Me.CheckBoxActivated)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Xl_Contact_Trp"
        Me.Size = New System.Drawing.Size(376, 272)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate()
    Public Event UpdateChanges()

    Private mDirty As Boolean
    Private mTrp As Transportista
    Private mAllowEvents As Boolean

    Public WriteOnly Property Transportista() As transportista
        Set(ByVal value As transportista)
            mTrp = value
            With mTrp
                CheckBoxActivated.Checked = .Activat
                TextBoxNom.Text = .Abr
                TextBoxCubicatje.Text = IIf(.Cubicaje = 0, "", .Cubicaje)
                TextBoxNoCubicarPerSotaDe.Text = IIf(.NoCubicarPerSotaDe = 0, "", .NoCubicarPerSotaDe)
                TextBoxFactor.Text = IIf(.CompensaPercent = 0, "", .CompensaPercent)
                CheckBoxAllowReembolsos.Checked = .AllowReembolsos
                CheckBoxTransportaMobiliari.Checked = .TransportaMobiliari
                FormatActivat()
            End With
            mAllowEvents = True
        End Set
    End Property

    Public ReadOnly Property Activated() As Boolean
        Get
            Return CheckBoxActivated.Checked
        End Get
    End Property

    Public ReadOnly Property Nom() As String
        Get
            Return TextBoxNom.Text
        End Get
     End Property

    Public ReadOnly Property Cubicatje() As Integer
        Get
            If IsNumeric(TextBoxCubicatje.Text) Then
                Return CInt(TextBoxCubicatje.Text)
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property Factor() As Decimal
        Get
            If IsNumeric(TextBoxFactor.Text) Then
                Return TextBoxFactor.Text
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property NoCubicarPerSotaDe() As Decimal
        Get
            If IsNumeric(TextBoxNoCubicarPerSotaDe.Text) Then
                Return CSng(TextBoxNoCubicarPerSotaDe.Text)
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property AllowReembolsos() As Boolean
        Get
            Return CheckBoxAllowReembolsos.Checked
        End Get
    End Property

    Public ReadOnly Property TransportaMobiliari() As Boolean
        Get
            Return CheckBoxTransportaMobiliari.Checked
        End Get
    End Property

    Private Sub CheckBoxActivated_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxActivated.CheckedChanged
        If mAllowEvents Then
            FormatActivat()
            SetDirty()
        End If
    End Sub

    Private Sub FormatActivat()
        Dim BlEnabled As Boolean = CheckBoxActivated.Checked
        TextBoxNom.Enabled = BlEnabled
        TextBoxCubicatje.Enabled = BlEnabled
        TextBoxNoCubicarPerSotaDe.Enabled = BlEnabled
        TextBoxFactor.Enabled = BlEnabled
        CheckBoxAllowReembolsos.Enabled = BlEnabled
        CheckBoxTransportaMobiliari.Enabled = BlEnabled
    End Sub

    Private Sub ControlValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles TextBoxNom.TextChanged, _
     TextBoxCubicatje.TextChanged, _
      TextBoxNoCubicarPerSotaDe.TextChanged, _
       TextBoxFactor.TextChanged, _
        CheckBoxAllowReembolsos.CheckedChanged, _
         CheckBoxTransportaMobiliari.CheckedChanged
        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub SetDirty()
        mDirty = True
        RaiseEvent AfterUpdate()
    End Sub


    Private Sub ButtonTarifas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonTarifas.Click
        If mDirty Then
            Dim rc As MsgBoxResult = MsgBox("Guardem primer els canvis?", MsgBoxStyle.OKCancel, "MAT.NET")
            If rc = MsgBoxResult.OK Then RaiseEvent UpdateChanges()
        End If
        root.ShowTrpTarifas(mTrp)
    End Sub
End Class
