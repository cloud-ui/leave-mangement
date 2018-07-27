<template>
    <div>
        <div>
            <pre v-html="formatText(this.notice.content)" ></pre>
        </div>
        <div style="text-align: right;">
            <p>{{this.notice.createTime}}</p>
            <p>{{this.notice.createHuman}}</p>
        </div>
    </div>
</template>
<script>
export default {
    props:['notice'],
    methods: {
        formatText(text){
                let num = -1,
                rHtml = new RegExp("\<.*?\>", "ig"), //匹配html标签元素
                aHtml = text.match(rHtml), //存放html元素的数组
                rNbsp = new RegExp("\&.*?\;", "ig"), //匹配&nbsp;
                aNbsp = text.match(rNbsp); //存放&nbsp;元素的数组
                text = text.replace(rHtml, '{~}'); //替换html标签为{~}
                text = text.replace(rNbsp, '{&}'); //替换&nbsp;标签为{&}
                text = text.replace(/{~}/g, function() { //恢复html标签
                    num++;
                    return aHtml[num];
                });
                num = -1;
                text = text.replace(/{&}/g, function() { //恢复&nbsp;标签
                    num++;
                    return aNbsp[num];
                });
                return text;
            }
        }
}
</script>
