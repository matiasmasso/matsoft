

Public Class Frm_AEB19
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNombrePresentador As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNIFPresentador As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSufijoPresentador As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxSufijoOrdenante As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNIFOrdenante As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNombreOrdenante As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxOficinaReceptora As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxEntitatReceptora As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerConfeccio As System.Windows.Forms.DateTimePicker
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemImportar As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemExportar As System.Windows.Forms.MenuItem
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickermFechaDeCargo As System.Windows.Forms.DateTimePicker
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TextBoxSufijoPresentador = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxNIFPresentador = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxNombrePresentador = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TextBoxSufijoOrdenante = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxNIFOrdenante = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBoxNombreOrdenante = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TextBoxOficinaReceptora = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBoxEntitatReceptora = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.DateTimePickerConfeccio = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItemImportar = New System.Windows.Forms.MenuItem
        Me.MenuItemExportar = New System.Windows.Forms.MenuItem
        Me.Label10 = New System.Windows.Forms.Label
        Me.DateTimePickermFechaDeCargo = New System.Windows.Forms.DateTimePicker
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBoxSufijoPresentador)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBoxNIFPresentador)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBoxNombrePresentador)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(528, 72)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Presentador:"
        '
        'TextBoxSufijoPresentador
        '
        Me.TextBoxSufijoPresentador.Location = New System.Drawing.Point(456, 40)
        Me.TextBoxSufijoPresentador.Name = "TextBoxSufijoPresentador"
        Me.TextBoxSufijoPresentador.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxSufijoPresentador.TabIndex = 5
        Me.TextBoxSufijoPresentador.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(400, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Sufixe:"
        '
        'TextBoxNIFPresentador
        '
        Me.TextBoxNIFPresentador.Location = New System.Drawing.Point(128, 40)
        Me.TextBoxNIFPresentador.Name = "TextBoxNIFPresentador"
        Me.TextBoxNIFPresentador.Size = New System.Drawing.Size(128, 20)
        Me.TextBoxNIFPresentador.TabIndex = 3
        Me.TextBoxNIFPresentador.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(72, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "NIF:"
        '
        'TextBoxNombrePresentador
        '
        Me.TextBoxNombrePresentador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNombrePresentador.Location = New System.Drawing.Point(128, 16)
        Me.TextBoxNombrePresentador.Name = "TextBoxNombrePresentador"
        Me.TextBoxNombrePresentador.Size = New System.Drawing.Size(384, 20)
        Me.TextBoxNombrePresentador.TabIndex = 1
        Me.TextBoxNombrePresentador.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(72, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nom:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.TextBoxSufijoOrdenante)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TextBoxNIFOrdenante)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.TextBoxNombreOrdenante)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 152)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(528, 72)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Ordenante:"
        '
        'TextBoxSufijoOrdenante
        '
        Me.TextBoxSufijoOrdenante.Location = New System.Drawing.Point(456, 40)
        Me.TextBoxSufijoOrdenante.Name = "TextBoxSufijoOrdenante"
        Me.TextBoxSufijoOrdenante.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxSufijoOrdenante.TabIndex = 5
        Me.TextBoxSufijoOrdenante.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(400, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Sufixe:"
        '
        'TextBoxNIFOrdenante
        '
        Me.TextBoxNIFOrdenante.Location = New System.Drawing.Point(128, 40)
        Me.TextBoxNIFOrdenante.Name = "TextBoxNIFOrdenante"
        Me.TextBoxNIFOrdenante.Size = New System.Drawing.Size(128, 20)
        Me.TextBoxNIFOrdenante.TabIndex = 3
        Me.TextBoxNIFOrdenante.Text = ""
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(72, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "NIF:"
        '
        'TextBoxNombreOrdenante
        '
        Me.TextBoxNombreOrdenante.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNombreOrdenante.Location = New System.Drawing.Point(128, 16)
        Me.TextBoxNombreOrdenante.Name = "TextBoxNombreOrdenante"
        Me.TextBoxNombreOrdenante.Size = New System.Drawing.Size(384, 20)
        Me.TextBoxNombreOrdenante.TabIndex = 1
        Me.TextBoxNombreOrdenante.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(72, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Nom:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.TextBoxOficinaReceptora)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TextBoxEntitatReceptora)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 232)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(528, 72)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entidad receptora:"
        '
        'TextBoxOficinaReceptora
        '
        Me.TextBoxOficinaReceptora.Location = New System.Drawing.Point(128, 40)
        Me.TextBoxOficinaReceptora.Name = "TextBoxOficinaReceptora"
        Me.TextBoxOficinaReceptora.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxOficinaReceptora.TabIndex = 3
        Me.TextBoxOficinaReceptora.Text = ""
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(72, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 16)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Oficina:"
        '
        'TextBoxEntitatReceptora
        '
        Me.TextBoxEntitatReceptora.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEntitatReceptora.Location = New System.Drawing.Point(128, 16)
        Me.TextBoxEntitatReceptora.Name = "TextBoxEntitatReceptora"
        Me.TextBoxEntitatReceptora.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxEntitatReceptora.TabIndex = 1
        Me.TextBoxEntitatReceptora.Text = ""
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(72, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 16)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Entitat:"
        '
        'DateTimePickerConfeccio
        '
        Me.DateTimePickerConfeccio.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.DateTimePickerConfeccio.Location = New System.Drawing.Point(456, 24)
        Me.DateTimePickerConfeccio.Name = "DateTimePickerConfeccio"
        Me.DateTimePickerConfeccio.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerConfeccio.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(344, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Data de presentació:"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemImportar, Me.MenuItemExportar})
        Me.MenuItem1.Text = "Arxiu"
        '
        'MenuItemImportar
        '
        Me.MenuItemImportar.Index = 0
        Me.MenuItemImportar.Text = "Importar"
        '
        'MenuItemExportar
        '
        Me.MenuItemExportar.Index = 1
        Me.MenuItemExportar.Text = "Exportar"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(344, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(112, 16)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Data de càrrec:"
        '
        'DateTimePickermFechaDeCargo
        '
        Me.DateTimePickermFechaDeCargo.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.DateTimePickermFechaDeCargo.Location = New System.Drawing.Point(456, 48)
        Me.DateTimePickermFechaDeCargo.Name = "DateTimePickermFechaDeCargo"
        Me.DateTimePickermFechaDeCargo.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickermFechaDeCargo.TabIndex = 5
        '
        'Frm_AEB19
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(560, 446)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DateTimePickermFechaDeCargo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DateTimePickerConfeccio)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Menu = Me.MainMenu1
        Me.Name = "Frm_AEB19"
        Me.Text = "REMESES CODI 19"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mAEB19 As Aeb19

    Public Property Aeb19() As AEB19
        Get
            GetDataFromControls()
            Return mAEB19
        End Get
        Set(ByVal Value As AEB19)
            If Not Value Is Nothing Then
                mAEB19 = Value
                Display()
            End If
        End Set
    End Property

    Sub Display()
        With maeb19
            TextBoxNIFPresentador.Text = .NIFPresentador
            TextBoxSufijoPresentador.Text = .SufijoPresentador
            TextBoxNombrePresentador.Text = .NombrePresentador
            TextBoxNIFOrdenante.Text = .NIFOrdenante
            TextBoxSufijoOrdenante.Text = .SufijoOrdenante
            TextBoxNombreOrdenante.Text = .NombreOrdenante
            DateTimePickerConfeccio.Value = .FechaDeConfeccion
            DateTimePickermFechaDeCargo.Value = .fechadecargo
            TextBoxEntitatReceptora.Text = .EntidadReceptora
            TextBoxOficinaReceptora.text = .OficinaReceptora
        End With
    End Sub


    Private Sub GetDataFromControls()
        With mAEB19
            .NIFPresentador = TextBoxNIFPresentador.Text
            .SufijoPresentador = TextBoxSufijoPresentador.Text
            .NombrePresentador = TextBoxNombrePresentador.Text
            .NIFOrdenante = TextBoxNIFOrdenante.Text
            .SufijoOrdenante = TextBoxSufijoOrdenante.Text
            .NombreOrdenante = TextBoxNombreOrdenante.Text
            .FechaDeConfeccion = DateTimePickerConfeccio.Value
            .FechaDeCargo = DateTimePickermFechaDeCargo.Value
            .EntidadReceptora = TextBoxEntitatReceptora.Text
            .OficinaReceptora = TextBoxOficinaReceptora.Text
        End With
    End Sub

    Private Sub MenuItemImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImportar.Click
        Dim oDlg As New Windows.Forms.OpenFileDialog
        Dim oResult As Windows.Forms.DialogResult
        With oDlg
            .Title = "OBRIR REMESES CODI 19"
            .Filter = "arxius de texte|*.txt|tots els arxius|*.*"
            '.FilterIndex = 3
            oResult = .ShowDialog
            Select Case oResult
                Case Windows.Forms.DialogResult.OK
                    mAEB19 = New AEB19(.FileName)
                    Display()
            End Select
        End With
    End Sub

    Private Sub MenuItemExportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExportar.Click
        Dim oDlg As New Windows.Forms.SaveFileDialog
        Dim oResult As Windows.Forms.DialogResult
        With oDlg
            .Title = "GUARDAR REMESES CODI 19"
            .Filter = "arxius de texte|*.txt|tots els arxius|*.*"
            '.FilterIndex = 1
            oResult = .ShowDialog
            Select Case oResult
                Case Windows.Forms.DialogResult.OK
                    GetDataFromControls()
                    mAEB19.Save(.FileName)
            End Select
        End With
    End Sub
End Class
