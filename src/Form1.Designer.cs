namespace src
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.browse_button = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.graph_file_label = new System.Windows.Forms.Label();
            this.algorithm_label = new System.Windows.Forms.Label();
            this.dfs_radbutton = new System.Windows.Forms.RadioButton();
            this.bfs_radbutton = new System.Windows.Forms.RadioButton();
            this.choose_account_label = new System.Windows.Forms.Label();
            this.graph_img_box = new System.Windows.Forms.PictureBox();
            this.explore_friends_with_label = new System.Windows.Forms.Label();
            this.choose_acc_combobox = new System.Windows.Forms.ComboBox();
            this.explore_friend_combobox = new System.Windows.Forms.ComboBox();
            this.submit_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.result_list = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.graph_img_box)).BeginInit();
            this.SuspendLayout();
            // 
            // browse_button
            // 
            this.browse_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.browse_button.Location = new System.Drawing.Point(260, 127);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(118, 27);
            this.browse_button.TabIndex = 0;
            this.browse_button.Text = "Browse File";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // title
            // 
            this.title.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.title.Location = new System.Drawing.Point(225, 21);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(176, 26);
            this.title.TabIndex = 1;
            this.title.Text = "Bacon Pancakes";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.title.Click += new System.EventHandler(this.label1_Click);
            // 
            // subtitle
            // 
            this.subtitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subtitle.AutoSize = true;
            this.subtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.subtitle.Location = new System.Drawing.Point(182, 47);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(286, 31);
            this.subtitle.TabIndex = 2;
            this.subtitle.Text = "People You May Know";
            this.subtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.subtitle.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // graph_file_label
            // 
            this.graph_file_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.graph_file_label.AutoSize = true;
            this.graph_file_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graph_file_label.Location = new System.Drawing.Point(82, 129);
            this.graph_file_label.Name = "graph_file_label";
            this.graph_file_label.Size = new System.Drawing.Size(71, 19);
            this.graph_file_label.TabIndex = 3;
            this.graph_file_label.Text = "Graph File";
            this.graph_file_label.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // algorithm_label
            // 
            this.algorithm_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.algorithm_label.AutoSize = true;
            this.algorithm_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.algorithm_label.Location = new System.Drawing.Point(82, 160);
            this.algorithm_label.Name = "algorithm_label";
            this.algorithm_label.Size = new System.Drawing.Size(70, 19);
            this.algorithm_label.TabIndex = 4;
            this.algorithm_label.Text = "Algorithm";
            // 
            // dfs_radbutton
            // 
            this.dfs_radbutton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dfs_radbutton.AutoSize = true;
            this.dfs_radbutton.Location = new System.Drawing.Point(260, 161);
            this.dfs_radbutton.Name = "dfs_radbutton";
            this.dfs_radbutton.Size = new System.Drawing.Size(45, 19);
            this.dfs_radbutton.TabIndex = 5;
            this.dfs_radbutton.TabStop = true;
            this.dfs_radbutton.Text = "DFS";
            this.dfs_radbutton.UseVisualStyleBackColor = true;
            this.dfs_radbutton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // bfs_radbutton
            // 
            this.bfs_radbutton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bfs_radbutton.AutoSize = true;
            this.bfs_radbutton.Location = new System.Drawing.Point(326, 160);
            this.bfs_radbutton.Name = "bfs_radbutton";
            this.bfs_radbutton.Size = new System.Drawing.Size(44, 19);
            this.bfs_radbutton.TabIndex = 6;
            this.bfs_radbutton.TabStop = true;
            this.bfs_radbutton.Text = "BFS";
            this.bfs_radbutton.UseVisualStyleBackColor = true;
            // 
            // choose_account_label
            // 
            this.choose_account_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.choose_account_label.AutoSize = true;
            this.choose_account_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.choose_account_label.Location = new System.Drawing.Point(82, 350);
            this.choose_account_label.Name = "choose_account_label";
            this.choose_account_label.Size = new System.Drawing.Size(109, 19);
            this.choose_account_label.TabIndex = 7;
            this.choose_account_label.Text = "Choose Account";
            this.choose_account_label.Click += new System.EventHandler(this.label1_Click_3);
            // 
            // graph_img_box
            // 
            this.graph_img_box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.graph_img_box.Location = new System.Drawing.Point(82, 199);
            this.graph_img_box.Name = "graph_img_box";
            this.graph_img_box.Size = new System.Drawing.Size(463, 145);
            this.graph_img_box.TabIndex = 8;
            this.graph_img_box.TabStop = false;
            // 
            // explore_friends_with_label
            // 
            this.explore_friends_with_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.explore_friends_with_label.AutoSize = true;
            this.explore_friends_with_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.explore_friends_with_label.Location = new System.Drawing.Point(82, 386);
            this.explore_friends_with_label.Name = "explore_friends_with_label";
            this.explore_friends_with_label.Size = new System.Drawing.Size(128, 19);
            this.explore_friends_with_label.TabIndex = 9;
            this.explore_friends_with_label.Text = "Explore friends with";
            this.explore_friends_with_label.Click += new System.EventHandler(this.label1_Click_4);
            // 
            // choose_acc_combobox
            // 
            this.choose_acc_combobox.FormattingEnabled = true;
            this.choose_acc_combobox.Location = new System.Drawing.Point(260, 350);
            this.choose_acc_combobox.Name = "choose_acc_combobox";
            this.choose_acc_combobox.Size = new System.Drawing.Size(121, 23);
            this.choose_acc_combobox.TabIndex = 10;
            this.choose_acc_combobox.Text = "A";
            // 
            // explore_friend_combobox
            // 
            this.explore_friend_combobox.FormattingEnabled = true;
            this.explore_friend_combobox.Location = new System.Drawing.Point(260, 382);
            this.explore_friend_combobox.Name = "explore_friend_combobox";
            this.explore_friend_combobox.Size = new System.Drawing.Size(121, 23);
            this.explore_friend_combobox.TabIndex = 11;
            this.explore_friend_combobox.Text = "B";
            // 
            // submit_button
            // 
            this.submit_button.Location = new System.Drawing.Point(82, 419);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(91, 30);
            this.submit_button.TabIndex = 12;
            this.submit_button.Text = "Submit";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(82, 468);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Friend recommendations for A :";
            this.label1.Click += new System.EventHandler(this.label1_Click_5);
            // 
            // result_list
            // 
            this.result_list.FormattingEnabled = true;
            this.result_list.ItemHeight = 15;
            this.result_list.Location = new System.Drawing.Point(82, 500);
            this.result_list.Name = "result_list";
            this.result_list.Size = new System.Drawing.Size(120, 94);
            this.result_list.TabIndex = 14;
            this.result_list.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 682);
            this.Controls.Add(this.result_list);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.explore_friend_combobox);
            this.Controls.Add(this.choose_acc_combobox);
            this.Controls.Add(this.explore_friends_with_label);
            this.Controls.Add(this.graph_img_box);
            this.Controls.Add(this.choose_account_label);
            this.Controls.Add(this.bfs_radbutton);
            this.Controls.Add(this.dfs_radbutton);
            this.Controls.Add(this.algorithm_label);
            this.Controls.Add(this.graph_file_label);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.title);
            this.Controls.Add(this.browse_button);
            this.Name = "Form1";
            this.Text = "Bacon Pancakes";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.graph_img_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button browse_button;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Label graph_file_label;
        private System.Windows.Forms.Label algorithm_label;
        private System.Windows.Forms.RadioButton dfs_radbutton;
        private System.Windows.Forms.RadioButton bfs_radbutton;
        private System.Windows.Forms.Label choose_account_label;
        private System.Windows.Forms.PictureBox graph_img_box;
        private System.Windows.Forms.Label explore_friends_with_label;
        private System.Windows.Forms.ComboBox choose_acc_combobox;
        private System.Windows.Forms.ComboBox explore_friend_combobox;
        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox result_list;
    }
}

