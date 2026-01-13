

Public Class Frm_ZonasEdit
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
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.ButtonRemove = New System.Windows.Forms.Button
        Me.ButtonAdd = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.Location = New System.Drawing.Point(8, 16)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(168, 199)
        Me.ListBox1.TabIndex = 0
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.Location = New System.Drawing.Point(264, 16)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(168, 199)
        Me.ListBox2.TabIndex = 1
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonRemove.Enabled = False
        Me.ButtonRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRemove.Location = New System.Drawing.Point(180, 16)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(80, 32)
        Me.ButtonRemove.TabIndex = 2
        Me.ButtonRemove.Text = "<"
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonAdd.Enabled = False
        Me.ButtonAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAdd.Location = New System.Drawing.Point(180, 48)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(80, 32)
        Me.ButtonAdd.TabIndex = 3
        Me.ButtonAdd.Text = ">"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(248, 240)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 24)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.TabStop = False
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(344, 240)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(88, 24)
        Me.ButtonOk.TabIndex = 5
        Me.ButtonOk.TabStop = False
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'Frm_ZonasEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(440, 270)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "Frm_ZonasEdit"
        Me.Text = "ZONES..."
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mZonas As List(Of Zona)
    Private mFreeZonas As List(Of Zona)
    Private mCancel As Boolean

    Public WriteOnly Property FreeZonas() As List(Of Zona)
        Set(value As List(Of Zona))
            mFreeZonas = value
            SetZonas(mFreeZonas, ListBox1)
        End Set
    End Property

    Public Property Zonas() As List(Of Zona)
        Get
            Return mZonas
        End Get
        Set(ByVal Value As List(Of Zona))
            mZonas = Value
            SetZonas(mZonas, ListBox2)
        End Set
    End Property

    Public ReadOnly Property Cancel() As Boolean
        Get
            Return mCancel
        End Get
    End Property

    Private Sub SetZonas(ByVal oZonas As List(Of Zona), ByVal oListBox As ListBox)
        Dim oZona As Zona
        oListBox.Items.Clear()
        For Each oZona In oZonas
            oListBox.Items.Add(oZona.Nom)
        Next
    End Sub

    Private Sub SetButtons()
        ButtonAdd.Enabled = ListBox1.Items.Count > 0
        ButtonRemove.Enabled = ListBox2.Items.Count > 0
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Dim Idx As Integer = ListBox1.SelectedIndex
        Dim oZona As Zona = mFreeZonas(Idx)
        mZonas.Add(oZona)
        mFreeZonas.RemoveAt(Idx)
        SetZonas(mFreeZonas, ListBox1)
        SetZonas(mZonas, ListBox2)
        SetButtons()
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRemove.Click
        Dim Idx As Integer = ListBox2.SelectedIndex
        Dim oZona As Zona = mZonas(Idx)
        mFreeZonas.Add(oZona)
        mZonas.RemoveAt(Idx)
        SetZonas(mFreeZonas, ListBox1)
        SetZonas(mZonas, ListBox2)
        SetButtons()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        mCancel = True
        Me.Close()
    End Sub
End Class
