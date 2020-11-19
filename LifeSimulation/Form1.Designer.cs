namespace LifeSimulation
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nudFood = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ApplyLifeSpeed = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLifeSpeed = new System.Windows.Forms.NumericUpDown();
            this.GenerateFood = new System.Windows.Forms.Button();
            this.infection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Resume = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.nudResolution = new System.Windows.Forms.NumericUpDown();
            this.resoltion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLifeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.nudFood);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.ApplyLifeSpeed);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.nudLifeSpeed);
            this.splitContainer1.Panel1.Controls.Add(this.GenerateFood);
            this.splitContainer1.Panel1.Controls.Add(this.infection);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.Resume);
            this.splitContainer1.Panel1.Controls.Add(this.Stop);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.nudResolution);
            this.splitContainer1.Panel1.Controls.Add(this.resoltion);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1372, 651);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 0;
            // 
            // nudFood
            // 
            this.nudFood.Location = new System.Drawing.Point(32, 484);
            this.nudFood.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudFood.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFood.Name = "nudFood";
            this.nudFood.Size = new System.Drawing.Size(149, 20);
            this.nudFood.TabIndex = 13;
            this.nudFood.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudFood.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(27, 456);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Generate Food";
            // 
            // ApplyLifeSpeed
            // 
            this.ApplyLifeSpeed.Location = new System.Drawing.Point(32, 393);
            this.ApplyLifeSpeed.Name = "ApplyLifeSpeed";
            this.ApplyLifeSpeed.Size = new System.Drawing.Size(149, 23);
            this.ApplyLifeSpeed.TabIndex = 11;
            this.ApplyLifeSpeed.Text = "Apply Life Speed";
            this.ApplyLifeSpeed.UseVisualStyleBackColor = true;
            this.ApplyLifeSpeed.Click += new System.EventHandler(this.ApplyLifeSpeed_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(48, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Life Speed";
            // 
            // nudLifeSpeed
            // 
            this.nudLifeSpeed.Location = new System.Drawing.Point(32, 367);
            this.nudLifeSpeed.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudLifeSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLifeSpeed.Name = "nudLifeSpeed";
            this.nudLifeSpeed.Size = new System.Drawing.Size(149, 20);
            this.nudLifeSpeed.TabIndex = 9;
            this.nudLifeSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLifeSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GenerateFood
            // 
            this.GenerateFood.Location = new System.Drawing.Point(32, 510);
            this.GenerateFood.Name = "GenerateFood";
            this.GenerateFood.Size = new System.Drawing.Size(149, 23);
            this.GenerateFood.TabIndex = 8;
            this.GenerateFood.Text = "Generate Food";
            this.GenerateFood.UseVisualStyleBackColor = true;
            this.GenerateFood.Click += new System.EventHandler(this.GenerateFood_Click);
            // 
            // infection
            // 
            this.infection.BackColor = System.Drawing.Color.DarkRed;
            this.infection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infection.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.infection.Location = new System.Drawing.Point(23, 561);
            this.infection.Name = "infection";
            this.infection.Size = new System.Drawing.Size(181, 69);
            this.infection.TabIndex = 7;
            this.infection.Text = "INFECT";
            this.infection.UseVisualStyleBackColor = false;
            this.infection.Click += new System.EventHandler(this.infection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(38, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Creature info";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(23, 229);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(172, 98);
            this.textBox1.TabIndex = 5;
            // 
            // Resume
            // 
            this.Resume.Location = new System.Drawing.Point(53, 144);
            this.Resume.Name = "Resume";
            this.Resume.Size = new System.Drawing.Size(120, 23);
            this.Resume.TabIndex = 4;
            this.Resume.Text = "Resume";
            this.Resume.UseVisualStyleBackColor = true;
            this.Resume.Click += new System.EventHandler(this.Resume_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(54, 115);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(120, 23);
            this.Stop.TabIndex = 3;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Stop_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Apply resolution";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nudResolution
            // 
            this.nudResolution.Location = new System.Drawing.Point(53, 37);
            this.nudResolution.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudResolution.Name = "nudResolution";
            this.nudResolution.Size = new System.Drawing.Size(120, 20);
            this.nudResolution.TabIndex = 1;
            this.nudResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudResolution.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // resoltion
            // 
            this.resoltion.AutoSize = true;
            this.resoltion.BackColor = System.Drawing.Color.Transparent;
            this.resoltion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resoltion.Location = new System.Drawing.Point(49, 9);
            this.resoltion.Name = "resoltion";
            this.resoltion.Size = new System.Drawing.Size(114, 25);
            this.resoltion.TabIndex = 0;
            this.resoltion.Text = "Resolution";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1100, 1000);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1372, 651);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLifeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label resoltion;
        private System.Windows.Forms.NumericUpDown nudResolution;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Resume;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button infection;
        private System.Windows.Forms.Button GenerateFood;
        private System.Windows.Forms.Button ApplyLifeSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudLifeSpeed;
        private System.Windows.Forms.NumericUpDown nudFood;
        private System.Windows.Forms.Label label3;
    }
}

