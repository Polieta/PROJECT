# Generated by Django 5.0.4 on 2024-05-30 03:20

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('TuiSach', '0005_remove_sanpham_hinh_anh_url_sanpham_hinh_anh'),
    ]

    operations = [
        migrations.AlterField(
            model_name='sanpham',
            name='hinh_anh',
            field=models.ImageField(blank=True, null=True, upload_to='static/banner/'),
        ),
    ]
