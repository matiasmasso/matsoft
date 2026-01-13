Public Class Frm_Regions_Select
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(292, 264)
        Me.ListBox1.TabIndex = 1
        '
        'Frm_Regions_Select
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "Frm_Regions_Select"
        Me.Text = "SELECCIONA REGIO..."
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private mIdx As Integer
    Private mRegions As MaxiSrvr.Regions

    Public Property Regio() As MaxiSrvr.Regio
        Get
            Return mRegions(mIdx)
        End Get
        Set(ByVal Value As MaxiSrvr.Regio)
            mIdx = ListBox1.FindStringExact(Value.Nom)
            If mIdx >= 0 Then ListBox1.SetSelected(mIdx, True)
        End Set
    End Property

    Public WriteOnly Property Regions() As MaxiSrvr.Regions
        Set(ByVal Value As MaxiSrvr.Regions)
            With ListBox1
                mRegions = Value
                Dim oRegio As MaxiSrvr.Regio
                For Each oRegio In mRegions
                    .Items.Add(oRegio.Nom)
                Next
            End With
        End Set
    End Property


    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Me.Close()
    End Sub

    Private Sub ListBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListBox1.KeyPress
        If Char.GetNumericValue(e.KeyChar) = 13 Then
            Me.Close()
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        mIdx = ListBox1.SelectedIndex
    End Sub
End Class
