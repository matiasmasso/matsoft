

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
    Friend WithEvents Label1Incoterms As System.Windows.Forms.Label
    Friend WithEvents GroupBoxImport As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxImportPrv As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxIrpfCod As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_LookupIncoterm1 As Xl_LookupIncoterm
    Friend WithEvents Xl_Cur1 As Xl_Cur
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Cur1 = New Winforms.Xl_Cur()
        Me.Xl_FormaDePago1 = New Winforms.Xl_FormaDePago()
        Me.Xl_Cta1 = New Winforms.Xl_Cta()
        Me.Label1Incoterms = New System.Windows.Forms.Label()
        Me.GroupBoxImport = New System.Windows.Forms.GroupBox()
        Me.CheckBoxImportPrv = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxIrpfCod = New System.Windows.Forms.ComboBox()
        Me.Xl_LookupIncoterm1 = New Winforms.Xl_LookupIncoterm()
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
        Me.Xl_Cur1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Xl_Cur1.Location = New System.Drawing.Point(72, 56)
        Me.Xl_Cur1.Name = "Xl_Cur1"
        Me.Xl_Cur1.ReadOnly = True
        Me.Xl_Cur1.Size = New System.Drawing.Size(30, 20)
        Me.Xl_Cur1.TabIndex = 3
        Me.Xl_Cur1.TabStop = False
        '
        'Xl_FormaDePago1
        '
        Me.Xl_FormaDePago1.BackColor = System.Drawing.SystemColors.Control
        Me.Xl_FormaDePago1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Xl_FormaDePago1.Location = New System.Drawing.Point(3, 240)
        Me.Xl_FormaDePago1.Name = "Xl_FormaDePago1"
        Me.Xl_FormaDePago1.Size = New System.Drawing.Size(292, 130)
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
        'Label1Incoterms
        '
        Me.Label1Incoterms.AutoSize = True
        Me.Label1Incoterms.Location = New System.Drawing.Point(6, 21)
        Me.Label1Incoterms.Name = "Label1Incoterms"
        Me.Label1Incoterms.Size = New System.Drawing.Size(48, 13)
        Me.Label1Incoterms.TabIndex = 63
        Me.Label1Incoterms.Text = "Incoterm"
        '
        'GroupBoxImport
        '
        Me.GroupBoxImport.Controls.Add(Me.Xl_LookupIncoterm1)
        Me.GroupBoxImport.Controls.Add(Me.Label1Incoterms)
        Me.GroupBoxImport.Location = New System.Drawing.Point(11, 138)
        Me.GroupBoxImport.Name = "GroupBoxImport"
        Me.GroupBoxImport.Size = New System.Drawing.Size(274, 79)
        Me.GroupBoxImport.TabIndex = 66
        Me.GroupBoxImport.TabStop = False
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
        'Xl_LookupIncoterm1
        '
        Me.Xl_LookupIncoterm1.FormattingEnabled = True
        Me.Xl_LookupIncoterm1.Location = New System.Drawing.Point(61, 18)
        Me.Xl_LookupIncoterm1.Name = "Xl_LookupIncoterm1"
        Me.Xl_LookupIncoterm1.Size = New System.Drawing.Size(58, 21)
        Me.Xl_LookupIncoterm1.TabIndex = 65
        Me.Xl_LookupIncoterm1.Value = Nothing
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
        Me.Size = New System.Drawing.Size(304, 383)
        Me.GroupBoxImport.ResumeLayout(False)
        Me.GroupBoxImport.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private _Proveidor As DTOProveidor
    Private _AllowEvents As Boolean


    Public Shadows Async Function Load(oProveidor As DTOProveidor) As Task
        Dim exs As New List(Of Exception)
        _Proveidor = oProveidor
        With _Proveidor
            Xl_Cta1.Cta = .defaultCtaCarrec
            Xl_Cur1.Cur = .Cur
            ComboBoxIrpfCod.SelectedIndex = .IRPF_Cod
            Await Xl_FormaDePago1.Load(DTOIban.Cods.proveidor, _Proveidor, .paymentTerms)

            Dim oIncoterms = Await FEB2.Incoterms.All(exs)
            If exs.Count = 0 Then
                Xl_LookupIncoterm1.Load(oIncoterms, .IncoTerm)
            End If

            If .IncoTerm IsNot Nothing Then
                CheckBoxImportPrv.Checked = True
                GroupBoxImport.Visible = True
            Else
                CheckBoxImportPrv.Checked = False
                GroupBoxImport.Visible = False
            End If

            _AllowEvents = True
        End With
    End Function


    Public ReadOnly Property Proveidor As DTOProveidor
        Get
            With _Proveidor
                .DefaultCtaCarrec = Xl_Cta1.Cta
                .Cur = Xl_Cur1.Cur
                .IRPF_Cod = ComboBoxIrpfCod.SelectedIndex
                .PaymentTerms = Xl_FormaDePago1.PaymentTerms
                If CheckBoxImportPrv.Checked Then
                    .IncoTerm = Xl_LookupIncoterm1.Value
                Else
                    .IncoTerm = Nothing
                End If
            End With
            Return _Proveidor
        End Get
    End Property


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        Xl_Cur1.AfterUpdate,
        Xl_Cta1.AfterUpdate,
        ComboBoxIrpfCod.SelectedIndexChanged,
        Xl_FormaDePago1.AfterUpdate,
        Xl_LookupIncoterm1.AfterUpdate

        If _AllowEvents Then
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Sub CheckBoxImportPrv_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxImportPrv.CheckedChanged
        If _AllowEvents Then
            GroupBoxImport.Visible = CheckBoxImportPrv.Checked
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

End Class
