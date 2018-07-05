<template>
<div>
<el-tree
    :data="data2"
    show-checkbox
    node-key="id"
    ref="tree"
    :default-expanded-keys="defaultCheckKeys"
    :default-checked-keys="defaultCheckKeys"
    :props="defaultProps">
</el-tree>
<div style="display: flex;
    justify-content: flex-end;
    padding-top: 10px;">
  <el-button :loading="loding" type="primary"  @click="submit()">提交</el-button>
</div>

</div>
    
</template>
<script>
import {FileApi} from '../api.js'
  export default {
    props:['positionId'],
    data() {
      return {
        data2: [],
        id:this.positionId,
        loding:false,
        defaultCheckKeys:[],
        defaultProps: {
          children: 'children',
          label: 'label'
        }
      }
    },
    watch:{
      positionId:{
        handler(val,oldVal){
          this.id = val
          this.loadSelectMenuId(this.id)
        },
        deep:true
      }
    },
    mounted(){
      this.loadMenuTree()
      this.loadSelectMenuId(this.id)
    },
    methods:{
      loadMenuTree(){
        FileApi.loadMenuTree().then(res=>{
          this.data2 = res.data
        })
      },
      loadSelectMenuId(val){
        FileApi.loadSelectMenuId(val).then(res=>{
          this.getIds(res.data)
        })
      },
      getIds(data){
        data.map(item=>{
          this.defaultCheckKeys.push(item.id);
          if(item.children.length != 0){
            item.children.map(value=>{
              this.defaultCheckKeys.push(value.id)
            })
          }
        })
      },
      submit(){
        const params={
          id:this.id,
          menuIds:this.$refs.tree.getCheckedKeys()
        }
        this.loding = true
        FileApi.SaveSelectMenu(params).then(res=>{
          const type1 = res.data.isSuccess?'success':'error'
          this.$message({
            type:type1,
            message:res.data.message
          })
          this.close
        })
      },
      close(){
        this.loding = false
        this.$emit('close',false)
      }
    }
  };
</script>

