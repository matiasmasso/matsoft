

Public Class Xl_Contact_Banc
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
    Friend WithEvents Xl_IBAN1 As Xl_IBAN
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxCedent As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCurClassificacio As Xl_AmountCur
    Friend WithEvents TextBoxSufixe As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxCuadraDisposat As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCurDisposat As Xl_AmountCur
    Friend WithEvents CheckBoxModeCcaImpags As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAbr As System.Windows.Forms.TextBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Xl_IBAN1 = New Xl_IBAN()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxModeCcaImpags = New System.Windows.Forms.CheckBox()
        Me.PictureBoxCuadraDisposat = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmtCurDisposat = New Xl_AmountCur()
        Me.TextBoxSufixe = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxCedent = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_AmtCurClassificacio = New Xl_AmountCur()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxAbr = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBoxCuadraDisposat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.Location = New System.Drawing.Point(33, 241)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 12
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxModeCcaImpags)
        Me.GroupBox1.Controls.Add(Me.PictureBoxCuadraDisposat)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtCurDisposat)
        Me.GroupBox1.Controls.Add(Me.TextBoxSufixe)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBoxCedent)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtCurClassificacio)
        Me.GroupBox1.Location = New System.Drawing.Point(33, 60)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(250, 153)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Norma 58"
        '
        'CheckBoxModeCcaImpags
        '
        Me.CheckBoxModeCcaImpags.AutoSize = True
        Me.CheckBoxModeCcaImpags.Location = New System.Drawing.Point(35, 121)
        Me.CheckBoxModeCcaImpags.Name = "CheckBoxModeCcaImpags"
        Me.CheckBoxModeCcaImpags.Size = New System.Drawing.Size(217, 17)
        Me.CheckBoxModeCcaImpags.TabIndex = 11
        Me.CheckBoxModeCcaImpags.Text = "separar nominal de despeses a impagats"
        Me.CheckBoxModeCcaImpags.UseVisualStyleBackColor = True
        '
        'PictureBoxCuadraDisposat
        '
        Me.PictureBoxCuadraDisposat.Image = My.Resources.Resources.warn
        Me.PictureBoxCuadraDisposat.Location = New System.Drawing.Point(88, 90)
        Me.PictureBoxCuadraDisposat.Name = "PictureBoxCuadraDisposat"
        Me.PictureBoxCuadraDisposat.Size = New System.Drawing.Size(17, 18)
        Me.PictureBoxCuadraDisposat.TabIndex = 13
        Me.PictureBoxCuadraDisposat.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Disposat:"
        '
        'Xl_AmtCurDisposat
        '
        Me.Xl_AmtCurDisposat.Amt = Nothing
        Me.Xl_AmtCurDisposat.Enabled = False
        Me.Xl_AmtCurDisposat.Location = New System.Drawing.Point(110, 89)
        Me.Xl_AmtCurDisposat.Name = "Xl_AmtCurDisposat"
        Me.Xl_AmtCurDisposat.Size = New System.Drawing.Size(125, 20)
        Me.Xl_AmtCurDisposat.TabIndex = 10
        '
        'TextBoxSufixe
        '
        Me.TextBoxSufixe.Location = New System.Drawing.Point(211, 36)
        Me.TextBoxSufixe.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxSufixe.Name = "TextBoxSufixe"
        Me.TextBoxSufixe.Size = New System.Drawing.Size(25, 20)
        Me.TextBoxSufixe.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(205, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Sufixe:"
        '
        'TextBoxCedent
        '
        Me.TextBoxCedent.Location = New System.Drawing.Point(32, 36)
        Me.TextBoxCedent.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxCedent.Name = "TextBoxCedent"
        Me.TextBoxCedent.Size = New System.Drawing.Size(172, 20)
        Me.TextBoxCedent.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Cedent:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Classificació:"
        '
        'Xl_AmtCurClassificacio
        '
        Me.Xl_AmtCurClassificacio.Amt = Nothing
        Me.Xl_AmtCurClassificacio.Location = New System.Drawing.Point(110, 63)
        Me.Xl_AmtCurClassificacio.Name = "Xl_AmtCurClassificacio"
        Me.Xl_AmtCurClassificacio.Size = New System.Drawing.Size(125, 20)
        Me.Xl_AmtCurClassificacio.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(65, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "abreviatura:"
        '
        'TextBoxAbr
        '
        Me.TextBoxAbr.Location = New System.Drawing.Point(143, 24)
        Me.TextBoxAbr.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxAbr.MaxLength = 10
        Me.TextBoxAbr.Name = "TextBoxAbr"
        Me.TextBoxAbr.Size = New System.Drawing.Size(140, 20)
        Me.TextBoxAbr.TabIndex = 1
        '
        'Xl_Contact_Banc
        '
        Me.Controls.Add(Me.TextBoxAbr)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Xl_IBAN1)
        Me.Name = "Xl_Contact_Banc"
        Me.Size = New System.Drawing.Size(342, 347)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBoxCuadraDisposat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Private mAllowEvents As Boolean
    Private mBanc As Banc

    Public WriteOnly Property Banc() As Banc
        Set(ByVal value As Banc)
            mBanc = value
            With mBanc
                TextBoxAbr.Text = .Abr
                TextBoxCedent.Text = .Norma58cedent
                TextBoxCedent.MaxLength = 20
                TextBoxSufixe.Text = .Norma58sufixe
                TextBoxSufixe.MaxLength = 3
                Xl_AmtCurClassificacio.Amt = .Classificacio
                Xl_AmtCurDisposat.Amt = .Norma58Risc
                PictureBoxCuadraDisposat.Image = IIf(.Norma58Cuadra, My.Resources.Ok, My.Resources.warn)
                CheckBoxModeCcaImpags.Checked = (.ModeCcaImpago = MaxiSrvr.Banc.ModesCcaImpago.SeparaNominalDeDespeses)
                Xl_IBAN1.Load(.Iban)
            End With
            mAllowEvents = True
        End Set
    End Property

    Public ReadOnly Property Abr As String
        Get
            Return TextBoxAbr.Text
        End Get
    End Property

    Public Property ModeCcaImpago() As MaxiSrvr.Banc.ModesCcaImpago
        Get
            Dim oRetVal As Banc.ModesCcaImpago = MaxiSrvr.Banc.ModesCcaImpago.Standard
            If CheckBoxModeCcaImpags.Checked Then oRetVal = MaxiSrvr.Banc.ModesCcaImpago.SeparaNominalDeDespeses
            Return oRetVal
        End Get
        Set(ByVal value As MaxiSrvr.Banc.ModesCcaImpago)
            CheckBoxModeCcaImpags.Checked = (value = MaxiSrvr.Banc.ModesCcaImpago.SeparaNominalDeDespeses)
        End Set
    End Property

    Public Property Iban() As DTOIban
        Get
            Dim retval As DTOIban = Xl_IBAN1.Value
            Return retval
        End Get
        Set(ByVal value As DTOIban)
            Xl_IBAN1.Load(value)
        End Set
    End Property

    Public Property Classificacio() As maxisrvr.Amt
        Get
            Return Xl_AmtCurClassificacio.Amt
        End Get
        Set(ByVal value As maxisrvr.Amt)
            Xl_AmtCurClassificacio.Amt = value
        End Set
    End Property

    Public Property Norma58Cedent() As String
        Get
            Return TextBoxCedent.Text
        End Get
        Set(ByVal value As String)
            TextBoxCedent.Text = value
            TextBoxCedent.MaxLength = 20
        End Set
    End Property

    Public Property Norma58Sufixe() As String
        Get
            Return TextBoxSufixe.Text
        End Get
        Set(ByVal value As String)
            TextBoxSufixe.Text = value
            TextBoxSufixe.MaxLength = 3
        End Set
    End Property


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxAbr.TextChanged, _
        TextBoxCedent.TextChanged, _
        TextBoxSufixe.TextChanged, _
        Xl_AmtCurClassificacio.AfterUpdate, _
        CheckBoxModeCcaImpags.CheckStateChanged

        If mAllowEvents Then
            RaiseEvent AfterUpdate(Me, Nothing)
        End If
    End Sub

    Private Sub Xl_IBAN1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_IBAN1.RequestToChange
        Dim oContact As DTOContact = New DTOContact(mBanc.Guid)
        Dim oFrm As New Frm_Contact_Ibans(oContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub

    Private Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim oContact As DTOContact = New DTOContact(mBanc.Guid)
        Dim oIban As DTOIban = BLL.BLLIban.FromContact(oContact)
        Xl_Iban1.Load(oIban)
    End Sub

End Class
