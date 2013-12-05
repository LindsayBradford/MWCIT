<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LauncherSelectionForm
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LauncherSelectionForm))
    Me.LauncherToolTips = New System.Windows.Forms.ToolTip(Me.components)
    Me.DynamicControlsPanel = New System.Windows.Forms.FlowLayoutPanel()
    Me.StatusStrip = New System.Windows.Forms.StatusStrip()
    Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
    Me.StatusStrip.SuspendLayout()
    Me.SuspendLayout()
    '
    'DynamicControlsPanel
    '
    Me.DynamicControlsPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DynamicControlsPanel.AutoScroll = True
    Me.DynamicControlsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.DynamicControlsPanel.BackColor = System.Drawing.SystemColors.Control
    Me.DynamicControlsPanel.Location = New System.Drawing.Point(1, 1)
    Me.DynamicControlsPanel.Name = "DynamicControlsPanel"
    Me.DynamicControlsPanel.Size = New System.Drawing.Size(393, 109)
    Me.DynamicControlsPanel.TabIndex = 1
    '
    'StatusStrip
    '
    Me.StatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
    Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
    Me.StatusStrip.Location = New System.Drawing.Point(0, 113)
    Me.StatusStrip.Name = "StatusStrip"
    Me.StatusStrip.Size = New System.Drawing.Size(394, 22)
    Me.StatusStrip.TabIndex = 2
    '
    'ToolStripStatusLabel
    '
    Me.ToolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control
    Me.ToolStripStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
    Me.ToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
    Me.ToolStripStatusLabel.Margin = New System.Windows.Forms.Padding(0, 3, 2, 0)
    Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
    Me.ToolStripStatusLabel.Size = New System.Drawing.Size(377, 19)
    Me.ToolStripStatusLabel.Spring = True
    Me.ToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'LauncherSelectionForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.ClientSize = New System.Drawing.Size(394, 135)
    Me.Controls.Add(Me.StatusStrip)
    Me.Controls.Add(Me.DynamicControlsPanel)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "LauncherSelectionForm"
    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Murrumbidgee Wetlands Condition Indicator Tool"
    Me.StatusStrip.ResumeLayout(False)
    Me.StatusStrip.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents LauncherToolTips As System.Windows.Forms.ToolTip
  Friend WithEvents DynamicControlsPanel As System.Windows.Forms.FlowLayoutPanel
  Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
  Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
End Class
