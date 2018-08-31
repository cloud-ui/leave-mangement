<template>
    <div>
        <div class="add-tip">
            <ul>
                <li>1.批量上传文件，需下载文件模板</li>
                <li>2.下载模板填写内容</li>
                <li>3.上传文件</li>
            </ul>
        </div>
        <div class="row-item">
            <div class="title">数据模板下载：</div>
            <div class="link">
              <a :class="linkA" href="http://localhost:57503/api/File/DownloadFile">点击下载模板</a>
            </div>
        </div>
        <div class="row-item">
            <div class="title">选择数据文件：</div>
            <div class="upload">
              <el-upload
                class="upload-demo"
                ref="upload"
                action="https://jsonplaceholder.typicode.com/posts/"
                accept=".xlsx"
                :on-change="handleChangeFile"
                :file-list="fileList"
                :show-file-list="false"
                :auto-upload="false">
                <el-button class="select-file-button" slot="trigger" plain >{{selectFile}}</el-button>
                <i class="el-icon-circle-close clear-file-button" v-show="clearFileIsShow" @click="clearFile"></i>
              <el-button class="import-data-button" plain @click="importData">导入数据</el-button>
              </el-upload>
            </div>
          </div>
    </div>
</template>
<script>
import '../dangan.scss'
import {FileApi} from '../api.js'
export default {
    data(){
        return{
            fileList:[],
            selectFile:'选择文件',
            clearFileIsShow:false,
            linkA:'link-click',
        }
    },
    methods:{
        //点击下载模板
        downloadModel(){
            FileApi.downloadModel().then(res=>{
                //window.open("http://"+res.data.content)
                window.URL.revokeObjectURL(res.data);
            })
        },
        //删除文件
        clearFile(file, fileList) {
            this.$refs.upload.clearFiles();
            this.selectFile = "选择文件"
            this.clearFileIsShow = false
        },
        //文件改变
        handleChangeFile(file, fileList) {
            this.fileList = fileList.slice(-1);
            this.selectFile = file.name
            this.clearFileIsShow = true
        },
        importData(){
            //判断文件类型
            var ext,idx
            idx = this.selectFile.lastIndexOf(".");
            if(idx != -1){
                ext = this.selectFile.substr(idx+1).toUpperCase();
                ext = ext.toLowerCase();
                if(ext!="xlsx"){
                this.$message.error('请选择.xlsx文件！');
                return;
                }
            }else{
                this.$message.error('请选择.xlsx文件！');
                return;
            }
            //上传文件部分
            var files = [];
            this.fileList.forEach(file => {
                files.push(file.raw);
            });
            console.log(files)
            FileApi.addMulitDeparment({},files).then(res=>{
                console.log(res)
            })
        }
    }
}
</script>
<style lang="scss" scoped>
.row-item{
      margin-bottom:10px;

      .title,.select,.link,.upload{
        display: inline-block;
        vertical-align: top;
      }

      .title {
        margin-right:20px;
        margin-top: 6px;
      }
      .select{
        margin-right:20px;
      }

      .link{
        margin-top: 6px;
        padding: 0px;
        .link-click{
          cursor:pointer;
          color: #09f
        }
      }

      .upload{
        position: relative;
      }

      .select-file-button{
        margin-right: -5px;
        padding: 10px 16px;
        width: 290px;
        border-top-right-radius:0;
        border-bottom-right-radius:0;
        text-align: left;
        text-overflow : ellipsis;
        white-space : nowrap;
        overflow : hidden;
      }

      .clear-file-button{
        position: absolute;
        top: 10px;
        left: 270px;
        color: #ccc8d1;
      }

      .clear-file-button:hover{
        color: #99959c;
        cursor:pointer
      }

      .import-data-button{
        border-top-left-radius:0;
        border-bottom-left-radius:0;
      }
    }
  
</style>


