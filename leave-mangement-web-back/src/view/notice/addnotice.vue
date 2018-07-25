<template>
    <div class="index">
        <div class="index-title">
            <p>添加公告</p>
        </div>
        <div class="index-body" :model="notice">
            <el-form label-width="80px" class="demo-ruleForm">
                <el-form-item label="公告标题:" prop="title">
                    <el-input v-model="notice.title" style="width:50%;"></el-input>
                </el-form-item>
                <el-form-item label="公告类型:" prop="type">
                    <el-radio v-model="notice.type" label="company" border size="medium">公司公告</el-radio>
                    <el-radio v-model="notice.type" label="deparment" border size="medium">部门公告</el-radio>
                </el-form-item>
                <el-form-item label="公告内容:" prop="content">
                    <comp-editor v-model="notice.content" style="height:300px;" ref="notice_editor"></comp-editor>
                </el-form-item>
                <el-form-item style="display: flex;justify-content: flex-end;">
                    <el-button>重置</el-button>
                    <el-button type="primary" :loading="loading" @click="submitData">提交</el-button>
                </el-form-item>
            </el-form>
            
        </div>
        
    </div>
</template>
<script>
import CompEditor from '../../packages/components/editor'
import '../index.scss'
export default {
    components:{
        CompEditor
    },
    data(){
        return{
            loading:false,
            notice:{
                title:'',
                type:'',
                content:''
            }
        }
    },
    methods:{
        submitData(){
            var noticeDetail = this.$refs["notice_editor"].getEditorContent();
            this.notice.content = noticeDetail
            console.log(this.notice)
        }
    }
}
</script>
