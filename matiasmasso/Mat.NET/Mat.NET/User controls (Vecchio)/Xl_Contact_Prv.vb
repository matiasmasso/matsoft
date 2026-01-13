

Public Class Xl_Contact_Prv
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
    Friend WithEvents Xl_Cta1 As Xl_Cta
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_FormaDePago1 As Xl_FormaDePago
    Friend WithEvents ComboBoxIncoterms As System.Windows.Forms.ComboBox
    Friend WithEvents Label1Incoterms As System.Windows.Forms.Label
    Friend WithEvents Label1CodiMercancia As System.Windows.Forms.Label
    Friend WithEvents GroupBoxImport As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxImportPrv As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_CodiMercancia1 As Xl_CodiMercancia
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxIrpfCod As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_Cur1 As Xl_Cur
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Cur1 = New Xl_Cur()
        Me.Xl_FormaDePago1 = New Xl_FormaDePago()
        Me.Xl_Cta1 = New Xl_Cta()
        Me.ComboBoxIncoterms = New System.Windows.Forms.ComboBox()
        Me.Label1Incoterms = New System.Windows.Forms.Label()
        Me.Label1CodiMercancia = New System.Windows.Forms.Label()
        Me.GroupBoxImport = New System.Windows.Forms.GroupBox()
        Me.Xl_CodiMercancia1 = New Xl_CodiMercancia()
        Me.CheckBoxImportPrv = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxIrpfCod = New System.Windows.Forms.ComboBox()
        Me.GroupBoxImport.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Compte:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Divisa:"
        '
        'Xl_Cur1
        '
        Me.Xl_Cur1.Cur = Nothing
        Me.Xl_Cur1.Location = New System.Drawing.Point(72, 56)
        Me.Xl_Cur1.Name = "Xl_Cur1"
        Me.Xl_Cur1.Size = New System.Drawing.Size(30, 20)
        Me.Xl_Cur1.TabIndex = 3
        '
        'Xl_FormaDePago1
        '
        Me.Xl_FormaDePago1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Xl_FormaDePago1.Location = New System.Drawing.Point(11, 243)
        Me.Xl_FormaDePago1.Name = "Xl_FormaDePago1"
        Me.Xl_FormaDePago1.Size = New System.Drawing.Size(284, 130)
        Me.Xl_FormaDePago1.TabIndex = 4
        '
        'Xl_Cta1
        '
        Me.Xl_Cta1.Cta = Nothing
        Me.Xl_Cta1.Location = New System.Drawing.Point(72, 32)
        Me.Xl_Cta1.Name = "Xl_Cta1"
        Me.Xl_Cta1.Size = New System.Drawing.Size(136, 20)
        Me.Xl_Cta1.TabIndex = 0
        '
        'ComboBoxIncoterms
        '
        Me.ComboBoxIncoterms.FormattingEnabled = True
        Me.ComboBoxIncoterms.Location = New System.Drawing.Point(70, 18)
        Me.ComboBoxIncoterms.Name = "ComboBoxIncoterms"
        Me.ComboBoxIncoterms.Size = New System.Drawing.Size(60, 21)
        Me.ComboBoxIncoterms.TabIndex = 64
        '
        'Label1Incoterms
        '
        Me.Label1Incoterms.AutoSize = True
        Me.Label1Incoterms.Location = New System.Drawing.Point(6, 21)
        Me.Label1Incoterms.Name = "Label1Incoterms"
        Me.Label1Incoterms.Size = New System.Drawing.Size(52, 13)
        Me.Label1Incoterms.TabIndex = 63
        Me.Label1Incoterms.Text = "incoterms"
        '
        'Label1CodiMercancia
        '
        Me.Label1CodiMercancia.AutoSize = True
        Me.Label1CodiMercancia.Location = New System.Drawing.Point(6, 48)
        Me.Label1CodiMercancia.Name = "Label1CodiMercancia"
        Me.Label1CodiMercancia.Size = New System.Drawing.Size(56, 13)
        Me.Label1CodiMercancia.TabIndex = 60
        Me.Label1CodiMercancia.Text = "mercancia"
        '
        'GroupBoxImport
        '
        Me.GroupBoxImport.Controls.Add(Me.Xl_CodiMercancia1)
        Me.GroupBoxImport.Controls.Add(Me.Label1CodiMercancia)
        Me.GroupBoxImport.Controls.Add(Me.ComboBoxIncoterms)
        Me.GroupBoxImport.Controls.Add(Me.Label1Incoterms)
        Me.GroupBoxImport.Location = New System.Drawing.Point(11, 138)
        Me.GroupBoxImport.Name = "GroupBoxImport"
        Me.GroupBoxImport.Size = New System.Drawing.Size(365, 79)
        Me.GroupBoxImport.TabIndex = 66
        Me.GroupBoxImport.TabStop = False
        '
        'Xl_CodiMercancia1
        '
        Me.Xl_CodiMercancia1.Location = New System.Drawing.Point(70, 45)
        Me.Xl_CodiMercancia1.Name = "Xl_CodiMercancia1"
        Me.Xl_CodiMercancia1.Size = New System.Drawing.Size(289, 20)
        Me.Xl_CodiMercancia1.TabIndex = 65
        '
        'CheckBoxImportPrv
        '
        Me.CheckBoxImportPrv.AutoSize = True
        Me.CheckBoxImportPrv.Location = New System.Drawing.Point(11, 126)
        Me.CheckBoxImportPrv.Name = "CheckBoxImportPrv"
        Me.CheckBoxImportPrv.Size = New System.Drawing.Size(129, 17)
        Me.CheckBoxImportPrv.TabIndex = 67
        Me.CheckBoxImportPrv.Text = "Mercancía importació"
        Me.CheckBoxImportPrv.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "Irpf:"
        '
        'ComboBoxIrpfCod
        '
        Me.ComboBoxIrpfCod.FormattingEnabled = True
        Me.ComboBoxIrpfCod.Items.AddRange(New Object() {"exempt", "estandar", "reduit", "altres"})
        Me.ComboBoxIrpfCod.Location = New System.Drawing.Point(72, 81)
        Me.ComboBoxIrpfCod.Name = "ComboBoxIrpfCod"
        Me.ComboBoxIrpfCod.Size = New System.Drawing.Size(136, 21)
        Me.ComboBoxIrpfCod.TabIndex = 69
        '
        'Xl_Contact_Prv
        '
        Me.Controls.Add(Me.ComboBoxIrpfCod)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckBoxImportPrv)
        Me.Controls.Add(Me.GroupBoxImport)
        Me.Controls.Add(Me.Xl_FormaDePago1)
        Me.Controls.Add(Me.Xl_Cur1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Cta1)
        Me.Name = "Xl_Contact_Prv"
        Me.Size = New System.Drawing.Size(379, 417)
        Me.GroupBoxImport.ResumeLayout(False)
        Me.GroupBoxImport.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private mEmp as DTOEmp
    Private mAllowEvents As Boolean
    Private mLoadedIncoterms As Boolean


    Public WriteOnly Property Proveidor() As Proveidor
        Set(ByVal value As Proveidor)
            With value
                mEmp = .Emp
                Xl_Cta1.Cta = .DefaultCtaCarrec
                Xl_Cur1.Cur = .DefaultCur
                ComboBoxIrpfCod.SelectedIndex = .IRPF_Cod
                Xl_FormaDePago1.LoadFromContact(Contact.Tipus.Proveidor, value, .FormaDePago)

                Dim BlIncoterms As Boolean = False
                If .Incoterm IsNot Nothing Then
                    BlIncoterms = .Incoterm.Exists
                End If

                If BlIncoterms Then
                    LoadIncoterms()
                    CheckBoxImportPrv.Checked = True
                    GroupBoxImport.Visible = True
                    ComboBoxIncoterms.SelectedValue = .Incoterm.Id
                    Xl_CodiMercancia1.CodiMercancia = .CodiMercancia
                Else
                    CheckBoxImportPrv.Checked = False
                    GroupBoxImport.Visible = False
                End If
            End With
            'Xl_IBAN1.IBAN = value.GetIBAN
            'LoadBancs()
            'ComboBoxNBanc.SelectedValue = value.Banc.Id
            mAllowEvents = True
        End Set
    End Property

    Public ReadOnly Property Cta() As PgcCta
        Get
            Return Xl_Cta1.Cta
        End Get
    End Property

    Public ReadOnly Property Cur() As maxisrvr.Cur
        Get
            Return Xl_Cur1.Cur
        End Get
    End Property

    Public ReadOnly Property Incoterm() As maxisrvr.IncoTerm
        Get
            If CheckBoxImportPrv.Checked Then
                Return New maxisrvr.IncoTerm(ComboBoxIncoterms.SelectedValue)
            Else
                Return New maxisrvr.IncoTerm(New String(" ", 3))
            End If
        End Get
    End Property

    Public ReadOnly Property CodiMercancia() As maxisrvr.CodiMercancia
        Get
            If CheckBoxImportPrv.Checked Then
                Return Xl_CodiMercancia1.CodiMercancia
            Else
                Return New maxisrvr.CodiMercancia(New String("0", 8))
            End If
        End Get
    End Property

    Public ReadOnly Property IRPF_Cod() As DTOProveidor.IRPFCods
        Get
            Dim retval As DTOProveidor.IRPFCods = ComboBoxIrpfCod.SelectedIndex
            Return retval
        End Get
    End Property

    Public ReadOnly Property FormaDePago() As FormaDePago
        Get
            Return Xl_FormaDePago1.FormaDePago
        End Get
    End Property

    Private Sub LoadIncoterms()
        Dim SQL As String = "SELECT ID FROM INCOTERMS ORDER BY ID"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxIncoterms
            .ValueMember = "ID"
            .DisplayMember = "ID"
            .DataSource = oDs.Tables(0)
        End With
        mLoadedIncoterms = True
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        Xl_Cur1.AfterUpdate, _
        Xl_Cta1.AfterUpdate, _
        Xl_CodiMercancia1.AfterUpdate, _
        ComboBoxIrpfCod.SelectedIndexChanged, _
        Xl_FormaDePago1.AfterUpdate, _
        ComboBoxIncoterms.SelectedIndexChanged
        If mAllowEvents Then
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Sub CheckBoxImportPrv_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxImportPrv.CheckedChanged
        If mAllowEvents Then
            If Not mLoadedIncoterms Then LoadIncoterms()
            GroupBoxImport.Visible = CheckBoxImportPrv.Checked
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

End Class
