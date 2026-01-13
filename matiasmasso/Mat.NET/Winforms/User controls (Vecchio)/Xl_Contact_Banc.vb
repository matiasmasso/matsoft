

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
    Friend WithEvents Xl_IBAN1 As Xl_Iban
    Friend WithEvents CheckBoxModeCcaImpags As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAbr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSepaCoreIdentificador As TextBox
    Friend WithEvents Label6 As Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CheckBoxModeCcaImpags = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxAbr = New System.Windows.Forms.TextBox()
        Me.TextBoxSepaCoreIdentificador = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_IBAN1 = New Winforms.Xl_Iban()
        Me.SuspendLayout()
        '
        'CheckBoxModeCcaImpags
        '
        Me.CheckBoxModeCcaImpags.AutoSize = True
        Me.CheckBoxModeCcaImpags.Location = New System.Drawing.Point(33, 118)
        Me.CheckBoxModeCcaImpags.Name = "CheckBoxModeCcaImpags"
        Me.CheckBoxModeCcaImpags.Size = New System.Drawing.Size(217, 17)
        Me.CheckBoxModeCcaImpags.TabIndex = 11
        Me.CheckBoxModeCcaImpags.Text = "separar nominal de despeses a impagats"
        Me.CheckBoxModeCcaImpags.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(69, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Abreviatura:"
        '
        'TextBoxAbr
        '
        Me.TextBoxAbr.Location = New System.Drawing.Point(143, 12)
        Me.TextBoxAbr.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxAbr.MaxLength = 10
        Me.TextBoxAbr.Name = "TextBoxAbr"
        Me.TextBoxAbr.Size = New System.Drawing.Size(140, 20)
        Me.TextBoxAbr.TabIndex = 1
        '
        'TextBoxSepaCoreIdentificador
        '
        Me.TextBoxSepaCoreIdentificador.Location = New System.Drawing.Point(143, 36)
        Me.TextBoxSepaCoreIdentificador.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxSepaCoreIdentificador.MaxLength = 16
        Me.TextBoxSepaCoreIdentificador.Name = "TextBoxSepaCoreIdentificador"
        Me.TextBoxSepaCoreIdentificador.Size = New System.Drawing.Size(140, 20)
        Me.TextBoxSepaCoreIdentificador.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Identificador Sepa Core:"
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.Location = New System.Drawing.Point(33, 62)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 12
        '
        'Xl_Contact_Banc
        '
        Me.Controls.Add(Me.CheckBoxModeCcaImpags)
        Me.Controls.Add(Me.TextBoxSepaCoreIdentificador)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxAbr)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_IBAN1)
        Me.Name = "Xl_Contact_Banc"
        Me.Size = New System.Drawing.Size(342, 417)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Private mAllowEvents As Boolean
    Private _Banc As DTOBanc

    Public Shadows Async Sub Load(oBanc As DTOBanc)
        _Banc = oBanc
        With _Banc
            TextBoxAbr.Text = .Abr
            TextBoxSepaCoreIdentificador.Text = .SepaCoreIdentificador
            CheckBoxModeCcaImpags.Checked = (.ModeCcaImpago = DTOBanc.ModesCcaImpago.SeparaNominalDeDespeses)
            Await Xl_IBAN1.Load(.Iban)
        End With
        mAllowEvents = True
    End Sub

    Public ReadOnly Property Banc As DTOBanc
        Get
            With _Banc
                .Abr = TextBoxAbr.Text
                .SepaCoreIdentificador = TextBoxSepaCoreIdentificador.Text
                .ModeCcaImpago = IIf(CheckBoxModeCcaImpags.Checked, DTOBanc.ModesCcaImpago.SeparaNominalDeDespeses, DTOBanc.ModesCcaImpago.Standard)
                .Iban = Xl_IBAN1.Value
            End With
            Return _Banc
        End Get
    End Property


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxAbr.TextChanged,
        TextBoxSepaCoreIdentificador.TextChanged,
        CheckBoxModeCcaImpags.CheckStateChanged

        If mAllowEvents Then
            RaiseEvent AfterUpdate(Me, Nothing)
        End If
    End Sub

    Private Sub Xl_IBAN1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_IBAN1.RequestToChange
        Dim oFrm As New Frm_Contact_Ibans(_Banc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub

    Private Async Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oIban = Await FEB2.Iban.FromContact(exs, _Banc, DTOIban.Cods.Banc)
        If exs.Count = 0 Then
            Await Xl_IBAN1.Load(oIban)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_IBAN1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_IBAN1.RequestToAddNew
        Dim oIban As New DTOIban
        With oIban
            .Cod = DTOIban.Cods.Banc
            .FchApproved = Today
            .Titular = _Banc
        End With
        Dim oFrm As New Frm_IbanCcc(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub
End Class
