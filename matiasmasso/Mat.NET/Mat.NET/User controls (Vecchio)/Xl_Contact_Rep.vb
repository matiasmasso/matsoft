Public Class Xl_Contact_Rep
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
    Friend WithEvents TextBoxAlias As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerBaja As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxBaja As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxComisio As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxComStandard As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxComReducida As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxIRPF As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCustomIRPF As System.Windows.Forms.TextBox
    Friend WithEvents Xl_IBAN1 As Xl_Iban
    Friend WithEvents CheckBoxFiscal As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_ContactFiscal As Xl_Contact
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Xl_RepProducts1 As Mat.NET.Xl_RepProducts
    Friend WithEvents GroupBoxFacturacio As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBoxAlias = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerBaja = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxBaja = New System.Windows.Forms.CheckBox()
        Me.GroupBoxComisio = New System.Windows.Forms.GroupBox()
        Me.TextBoxComReducida = New System.Windows.Forms.TextBox()
        Me.TextBoxComStandard = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox()
        Me.ComboBoxIRPF = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxCustomIRPF = New System.Windows.Forms.TextBox()
        Me.CheckBoxFiscal = New System.Windows.Forms.CheckBox()
        Me.Xl_Image1 = New Mat.NET.Xl_Image()
        Me.Xl_IBAN1 = New Mat.NET.Xl_Iban()
        Me.Xl_ContactFiscal = New Mat.NET.Xl_Contact()
        Me.GroupBoxFacturacio = New System.Windows.Forms.GroupBox()
        Me.Xl_RepProducts1 = New Mat.NET.Xl_RepProducts()
        Me.GroupBoxComisio.SuspendLayout()
        Me.GroupBoxFacturacio.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxAlias
        '
        Me.TextBoxAlias.Location = New System.Drawing.Point(88, 0)
        Me.TextBoxAlias.Name = "TextBoxAlias"
        Me.TextBoxAlias.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxAlias.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Alias:"
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(88, 24)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerAlta.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(24, 30)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Alta:"
        '
        'DateTimePickerBaja
        '
        Me.DateTimePickerBaja.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaja.Location = New System.Drawing.Point(88, 48)
        Me.DateTimePickerBaja.Name = "DateTimePickerBaja"
        Me.DateTimePickerBaja.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerBaja.TabIndex = 4
        '
        'CheckBoxBaja
        '
        Me.CheckBoxBaja.Location = New System.Drawing.Point(8, 48)
        Me.CheckBoxBaja.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxBaja.Name = "CheckBoxBaja"
        Me.CheckBoxBaja.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxBaja.TabIndex = 5
        Me.CheckBoxBaja.Text = "Baja"
        '
        'GroupBoxComisio
        '
        Me.GroupBoxComisio.Controls.Add(Me.TextBoxComReducida)
        Me.GroupBoxComisio.Controls.Add(Me.TextBoxComStandard)
        Me.GroupBoxComisio.Controls.Add(Me.Label4)
        Me.GroupBoxComisio.Controls.Add(Me.Label3)
        Me.GroupBoxComisio.Location = New System.Drawing.Point(8, 80)
        Me.GroupBoxComisio.Name = "GroupBoxComisio"
        Me.GroupBoxComisio.Size = New System.Drawing.Size(168, 80)
        Me.GroupBoxComisio.TabIndex = 6
        Me.GroupBoxComisio.TabStop = False
        Me.GroupBoxComisio.Text = "Comisions:"
        '
        'TextBoxComReducida
        '
        Me.TextBoxComReducida.Location = New System.Drawing.Point(84, 41)
        Me.TextBoxComReducida.Name = "TextBoxComReducida"
        Me.TextBoxComReducida.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxComReducida.TabIndex = 7
        '
        'TextBoxComStandard
        '
        Me.TextBoxComStandard.Location = New System.Drawing.Point(84, 19)
        Me.TextBoxComStandard.Name = "TextBoxComStandard"
        Me.TextBoxComStandard.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxComStandard.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(24, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Reducida:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(24, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Standard:"
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIVA.Location = New System.Drawing.Point(12, 48)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(54, 16)
        Me.CheckBoxIVA.TabIndex = 7
        Me.CheckBoxIVA.Text = "IVA"
        '
        'ComboBoxIRPF
        '
        Me.ComboBoxIRPF.FormattingEnabled = True
        Me.ComboBoxIRPF.Items.AddRange(New Object() {"exento", "standard", "custom"})
        Me.ComboBoxIRPF.Location = New System.Drawing.Point(51, 69)
        Me.ComboBoxIRPF.Name = "ComboBoxIRPF"
        Me.ComboBoxIRPF.Size = New System.Drawing.Size(72, 21)
        Me.ComboBoxIRPF.TabIndex = 8
        Me.ComboBoxIRPF.Text = "standard"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(12, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "IRPF:"
        '
        'TextBoxCustomIRPF
        '
        Me.TextBoxCustomIRPF.Location = New System.Drawing.Point(129, 68)
        Me.TextBoxCustomIRPF.Name = "TextBoxCustomIRPF"
        Me.TextBoxCustomIRPF.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxCustomIRPF.TabIndex = 10
        Me.TextBoxCustomIRPF.Text = "0"
        Me.TextBoxCustomIRPF.Visible = False
        '
        'CheckBoxFiscal
        '
        Me.CheckBoxFiscal.AutoSize = True
        Me.CheckBoxFiscal.Location = New System.Drawing.Point(8, 264)
        Me.CheckBoxFiscal.Name = "CheckBoxFiscal"
        Me.CheckBoxFiscal.Size = New System.Drawing.Size(159, 17)
        Me.CheckBoxFiscal.TabIndex = 17
        Me.CheckBoxFiscal.Text = "factures a un altre raó social"
        Me.CheckBoxFiscal.UseVisualStyleBackColor = True
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = "imatge 350x400 px"
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(296, 287)
        Me.Xl_Image1.MaxHeight = 0
        Me.Xl_Image1.MaxWidth = 0
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(173, 199)
        Me.Xl_Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Xl_Image1.TabIndex = 19
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_IBAN1.Location = New System.Drawing.Point(6, 94)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 14
        '
        'Xl_ContactFiscal
        '
        Me.Xl_ContactFiscal.Contact = Nothing
        Me.Xl_ContactFiscal.Location = New System.Drawing.Point(8, 287)
        Me.Xl_ContactFiscal.Name = "Xl_ContactFiscal"
        Me.Xl_ContactFiscal.ReadOnly = False
        Me.Xl_ContactFiscal.Size = New System.Drawing.Size(263, 20)
        Me.Xl_ContactFiscal.TabIndex = 18
        Me.Xl_ContactFiscal.Visible = False
        '
        'GroupBoxFacturacio
        '
        Me.GroupBoxFacturacio.Controls.Add(Me.Xl_IBAN1)
        Me.GroupBoxFacturacio.Controls.Add(Me.ComboBoxIRPF)
        Me.GroupBoxFacturacio.Controls.Add(Me.CheckBoxIVA)
        Me.GroupBoxFacturacio.Controls.Add(Me.Label5)
        Me.GroupBoxFacturacio.Controls.Add(Me.TextBoxCustomIRPF)
        Me.GroupBoxFacturacio.Location = New System.Drawing.Point(8, 313)
        Me.GroupBoxFacturacio.Name = "GroupBoxFacturacio"
        Me.GroupBoxFacturacio.Size = New System.Drawing.Size(263, 173)
        Me.GroupBoxFacturacio.TabIndex = 20
        Me.GroupBoxFacturacio.TabStop = False
        Me.GroupBoxFacturacio.Text = "facturació i pagos"
        '
        'Xl_RepProducts1
        '
        Me.Xl_RepProducts1.Location = New System.Drawing.Point(183, 0)
        Me.Xl_RepProducts1.Name = "Xl_RepProducts1"
        Me.Xl_RepProducts1.Size = New System.Drawing.Size(286, 281)
        Me.Xl_RepProducts1.TabIndex = 21
        '
        'Xl_Contact_Rep
        '
        Me.Controls.Add(Me.Xl_RepProducts1)
        Me.Controls.Add(Me.GroupBoxFacturacio)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.Xl_ContactFiscal)
        Me.Controls.Add(Me.CheckBoxFiscal)
        Me.Controls.Add(Me.GroupBoxComisio)
        Me.Controls.Add(Me.CheckBoxBaja)
        Me.Controls.Add(Me.DateTimePickerBaja)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerAlta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxAlias)
        Me.Name = "Xl_Contact_Rep"
        Me.Size = New System.Drawing.Size(477, 506)
        Me.GroupBoxComisio.ResumeLayout(False)
        Me.GroupBoxComisio.PerformLayout()
        Me.GroupBoxFacturacio.ResumeLayout(False)
        Me.GroupBoxFacturacio.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event Changed()

    Private mRep As Rep
    Private mAllowEvents As Boolean

    Public WriteOnly Property Rep() As MaxiSrvr.Rep
        Set(ByVal value As MaxiSrvr.Rep)
            mRep = value
            refrescaRepProducts()
        End Set
    End Property

    Public Property Abr() As String
        Get
            Return TextBoxAlias.Text
        End Get
        Set(ByVal value As String)
            TextBoxAlias.Text = value
        End Set
    End Property

    Public Property FchAlta() As Date
        Get
            Return DateTimePickerAlta.Value
        End Get
        Set(ByVal value As Date)
            If Not IsDate(value) Then value = DateTime.MinValue
            If value > DateTimePickerAlta.MinDate Then
                DateTimePickerAlta.Value = value
            Else
                DateTimePickerAlta.Value = Today
            End If
        End Set
    End Property

    Public Property FchBaja() As Date
        Get
            If CheckBoxBaja.Checked Then
                Return DateTimePickerBaja.Value
            Else
                Return DateTime.MinValue
            End If
        End Get
        Set(ByVal value As Date)
            If Not IsDate(value) Then value = DateTime.MinValue
            If value <= DateTimePickerBaja.MinDate Then
                CheckBoxBaja.Checked = False
                DateTimePickerBaja.Visible = False
            Else
                CheckBoxBaja.Checked = True
                DateTimePickerBaja.Visible = True
                DateTimePickerBaja.Value = value
            End If
        End Set
    End Property

    Public Property ComisionStandard() As Decimal
        Get
            If IsNumeric(TextBoxComStandard.Text) Then
                Return TextBoxComStandard.Text
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Decimal)
            TextBoxComStandard.Text = value
        End Set
    End Property

    Public Property ComisionReducida() As Decimal
        Get
            If IsNumeric(TextBoxComReducida.Text) Then
                Return TextBoxComReducida.Text
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Decimal)
            TextBoxComReducida.Text = value
        End Set
    End Property

    Public Property IVA() As MaxiSrvr.Rep.IVA_Cods
        Get
            Return IIf(CheckBoxIVA.Checked, MaxiSrvr.Rep.IVA_Cods.standard, MaxiSrvr.Rep.IVA_Cods.exento)
        End Get
        Set(ByVal value As MaxiSrvr.Rep.IVA_Cods)
            CheckBoxIVA.Checked = (value = MaxiSrvr.Rep.IVA_Cods.standard)
        End Set
    End Property

    Public Property IRPF() As DTOProveidor.IRPFCods
        Get
            Return ComboBoxIRPF.SelectedIndex
        End Get
        Set(ByVal value As DTOProveidor.IRPFCods)
            ComboBoxIRPF.SelectedIndex = CInt(value)
            TextBoxCustomIRPF.Visible = (value = DTOProveidor.IRPFCods.custom)
        End Set
    End Property

    Public Property IRPF_custom() As Decimal
        Get
            Return TextBoxCustomIRPF.Text
        End Get
        Set(ByVal value As Decimal)
            TextBoxCustomIRPF.Visible = value
        End Set
    End Property

    Public Property Ccx() As Contact
        Get
            If CheckBoxFiscal.Checked Then
                Return Xl_ContactFiscal.Contact
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Contact)
            If value IsNot Nothing Then
                If value.Id > 0 Then
                    CheckBoxFiscal.Checked = True
                    Xl_ContactFiscal.Visible = True
                    Xl_ContactFiscal.Contact = value
                End If
            End If
        End Set
    End Property

    Public Property IBAN() As DTOIban
        Get
            Dim retval As DTOIban = Xl_IBAN1.Value
            Return retval
        End Get
        Set(ByVal value As DTOIban)
            Xl_IBAN1.Load(value)
        End Set
    End Property

    Public Property Foto() As Image
        Get
            Return Xl_Image1.Bitmap
        End Get
        Set(ByVal value As Image)
            Xl_Image1.Bitmap = value
        End Set
    End Property

    Public Sub AllowEvents()
        mAllowEvents = True
    End Sub

    Private Sub DirtyControl(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxAlias.TextChanged, DateTimePickerAlta.ValueChanged, DateTimePickerBaja.ValueChanged, TextBoxComReducida.TextChanged, TextBoxComStandard.TextChanged, CheckBoxIVA.CheckedChanged, ComboBoxIRPF.SelectedIndexChanged, TextBoxCustomIRPF.TextChanged
        If mAllowEvents Then
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub Xl_Bnc1_Changed()
        If mAllowEvents Then
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub CheckBoxBaja_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBaja.CheckedChanged
        DateTimePickerBaja.Visible = CheckBoxBaja.Checked
        If mAllowEvents Then
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub Xl_RepProductsxRep1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepProducts1.RequestToRefresh
        refrescaRepProducts()
    End Sub

    Private Sub refrescaRepProducts()
        Dim oRep As New DTORep(mRep.Guid)
        Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(BLL.BLLApp.Emp, oRep)
        Xl_RepProducts1.Load(oRepProducts, Xl_RepProducts.Modes.ByRep)
    End Sub

    Private Sub CheckBoxFiscal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFiscal.CheckedChanged
        If mAllowEvents Then
            Xl_ContactFiscal.Visible = CheckBoxFiscal.Checked
            GroupBoxFacturacio.Visible = Not CheckBoxFiscal.Checked
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub Xl_ContactFiscal_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactFiscal.AfterUpdate
        If mAllowEvents Then
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub Xl_Image1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Image1.AfterUpdate
        If mAllowEvents Then
            RaiseEvent Changed()
        End If
    End Sub



    Private Sub Xl_IBAN1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_IBAN1.RequestToChange
        Dim oContact As DTOContact = New DTOContact(mRep.Guid)
        Dim oFrm As New Frm_Contact_Ibans(oContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub

    Private Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim oContact As DTOContact = New DTOContact(mRep.Guid)
        Dim oIban As DTOIban = BLL.BLLIban.FromContact(oContact)
        Xl_IBAN1.Load(oIban)
    End Sub
End Class
