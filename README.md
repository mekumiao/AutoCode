# AutoCode
代码生成器

## 语法

#### 1.输出

~~~html
//_data为变量名称
{{_data.name}}
~~~

#### 2.循环

~~~html
//val为遍历值，key为索引，从0开始
{{each _data.arrays val key}}
public class {{val.type}} {{val.name}}//输出
{{/each}}
~~~

#### 3.条件

~~~html
{{if _data.index > 0}}
...
{{/if}}

{{if _data.count >10}}
...
{{else if _data.count > 5}}
...
{{else}}
...
{{/if}}
~~~

#### 4.变量声明

~~~html
{{set i = 0}}
~~~

#### 5.全局对象

~~~html
//为根对象.(不能为数组)
_data
~~~