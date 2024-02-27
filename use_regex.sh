#!/bin/sh
find . -type f -name '*StepDefinitions.cs' -exec sed -i.bak 's/{int}/(.*)/g' {} \;
find . -type f -name '*StepDefinitions.cs' -exec sed -i.bak 's/{string}/\\"(.*)\\"/g' {} \;
find . -type f -name '*StepDefinitions.cs' -exec sed -i.bak 's/{DateTime}/(.*?)/g' {} \;
find . -type f -name '*StepDefinitions.cs' -exec sed -i.bak 's/{TimeSpan}/(.*?)/g' {} \;
find . -type f -name '*.feature' -exec sed -i.bak "s/'Trillian'/XTrillianX/g" {} \;
find . -type f -name '*.feature' -exec sed -i.bak 's/XTrillianX/"Trillian"/g' {} \;
find . -type f -name '*.feature' -exec sed -i.bak "s/'139139'/X139139X/g" {} \;
find . -type f -name '*.feature' -exec sed -i.bak 's/X139139X/"139139"/g' {} \;
find . -name "*.bak" -type f -delete
